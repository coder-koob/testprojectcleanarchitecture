using Application.ReadModels;
using Domain.Doors;
using Domain.Doors.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Doors.Events;

public class SetOfficeReadModelDoorsEventHandler : INotificationHandler<DoorAddedEvent>
{
    private readonly IReadModelService<OfficeReadModel> _officeReadModelService;

    public SetOfficeReadModelDoorsEventHandler(IReadModelService<OfficeReadModel> officeReadModelService)
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

        readModel.Doors.Add(new Door(notification.OfficeId, notification.DoorId, notification.Name));

        await _officeReadModelService.SaveAsync(readModel, notification.OfficeId.ToString());
    }
}