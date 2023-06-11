using Application.Doors.Models;
using Application.Doors.ReadModels;
using Domain.Doors.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Doors.Events;

public class AddDoorHistoryEventHandler: INotificationHandler<DoorAddedEvent>, INotificationHandler<DoorLockedEvent>, INotificationHandler<DoorUnlockedEvent>
{
    private readonly IReadModelService<DoorHistoryReadModel> _doorHistoryReadModelService;

    public AddDoorHistoryEventHandler(IReadModelService<DoorHistoryReadModel> doorHistoryReadModelService)
    {
        _doorHistoryReadModelService = doorHistoryReadModelService;
    }

    public async Task Handle(DoorAddedEvent notification, CancellationToken cancellationToken)
    {
        await Handle(notification.OfficeId, notification.DoorId, nameof(DoorAddedEvent), notification.Timestamp);
    }

    public async Task Handle(DoorLockedEvent notification, CancellationToken cancellationToken)
    {
        await Handle(notification.OfficeId, notification.DoorId, nameof(DoorLockedEvent), notification.Timestamp);
    }

    public async Task Handle(DoorUnlockedEvent notification, CancellationToken cancellationToken)
    {
        await Handle(notification.OfficeId, notification.DoorId, nameof(DoorUnlockedEvent), notification.Timestamp);
    }

    private async Task Handle(Guid officeId, Guid doorId, string eventName, DateTimeOffset timestamp)
    {
        var readModel = await _doorHistoryReadModelService.GetByIdAsync(doorId.ToString());

        readModel ??= new DoorHistoryReadModel();

        readModel.OfficeId = officeId;
        readModel.DoorId = doorId;
        readModel.Timestamp = timestamp;

        readModel.DoorHistory.Add(new DoorEventDto(eventName, timestamp));

        await _doorHistoryReadModelService.SaveAsync(readModel, doorId.ToString());
    }
}