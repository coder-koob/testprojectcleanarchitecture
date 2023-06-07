using Domain.Common;

namespace Domain.Doors.Events;

public class DoorLockedEvent : Event
{
    public Guid DoorId { get; }
    public Guid OfficeId { get; }

    public DoorLockedEvent(Guid officeId, Guid doorId)
    {
        OfficeId = officeId;
        DoorId = doorId;
    }
}