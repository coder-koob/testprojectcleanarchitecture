using Application.Doors.Models;
using Application.Offices.ReadModels;
using Domain.Doors.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Doors.Events;

public class SetOfficeDoorLockedEventHandler : INotificationHandler<DoorLockedEvent>
{
    private readonly IReadModelService<OfficeReadModel> _officeReadModelService;

    public SetOfficeDoorLockedEventHandler(IReadModelService<OfficeReadModel> officeReadModelService)
    {
        _officeReadModelService = officeReadModelService;
    }

    public async Task Handle(DoorLockedEvent notification, CancellationToken cancellationToken)
    {
        var readModel = await _officeReadModelService.GetByIdAsync(notification.OfficeId.ToString());

        readModel ??= new OfficeReadModel
        {
            OfficeId = notification.OfficeId,
        };

        var door = readModel.Doors.FirstOrDefault(x => x.DoorId == notification.DoorId);

        door ??= new DoorDto(notification.OfficeId, notification.DoorId, notification.Timestamp);

        door.IsLocked = true;

        readModel.Timestamp = notification.Timestamp;

        await _officeReadModelService.SaveAsync(readModel, notification.OfficeId.ToString());
    }
}