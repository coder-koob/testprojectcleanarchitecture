using Application.Doors.Models;
using Application.ReadModels;
using Domain.Doors.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Doors.Events;

public class AddOfficeDoorsEventHandler : INotificationHandler<DoorAddedEvent>
{
    private readonly IReadModelService<OfficeReadModel> _officeReadModelService;

    public AddOfficeDoorsEventHandler(IReadModelService<OfficeReadModel> officeReadModelService)
    {
        _officeReadModelService = officeReadModelService;
    }

    public async Task Handle(DoorAddedEvent notification, CancellationToken cancellationToken)
    {
        var readModel = await _officeReadModelService.GetByIdAsync(notification.OfficeId.ToString());

        readModel ??= new OfficeReadModel
        {
            OfficeId = notification.OfficeId,
        };

        var door = readModel.Doors.FirstOrDefault(x => x.DoorId == notification.DoorId);

        if (door is null)
        {
            readModel.Doors.Add(new DoorDto(notification.OfficeId, notification.DoorId, notification.Name, false));
        }
        else
        {
            door.Name = notification.Name;
        }

        await _officeReadModelService.SaveAsync(readModel, notification.OfficeId.ToString());
    }
}