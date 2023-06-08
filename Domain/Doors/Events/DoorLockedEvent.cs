using Domain.Common;

namespace Domain.Doors.Events;

public class DoorLockedEvent : Event
{
    public DoorLockedEvent(Guid officeId, Guid doorId)
        : base(officeId)
    {
        OfficeId = officeId;
        DoorId = doorId;
    }

    public Guid DoorId { get; }
    public Guid OfficeId { get; }
}