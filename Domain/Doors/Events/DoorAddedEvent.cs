using Domain.Common;

namespace Domain.Doors.Events;

public class DoorAddedEvent : Event
{
    public Guid DoorId { get; }

    public DoorAddedEvent(Guid officeId, Guid doorId)
    {
        DoorId = doorId;
        AggregateId = officeId;
    }
}
