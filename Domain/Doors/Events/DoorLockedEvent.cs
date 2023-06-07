using Domain.Common;

namespace Domain.Doors.Events;

public class DoorLockedEvent : Event
{
    public Guid OfficeId { get; }

    public DoorLockedEvent(Guid officeId, Guid doorId)
    {
        OfficeId = officeId;
        Id = doorId;
    }
}