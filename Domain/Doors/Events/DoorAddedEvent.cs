using Domain.Common;

namespace Domain.Doors.Events;

public class DoorAddedEvent : Event
{
    public Guid OfficeId { get; }

    public DoorAddedEvent(Guid officeId, Guid doorId)
    {
        OfficeId = officeId;
        Id = doorId;
    }
}