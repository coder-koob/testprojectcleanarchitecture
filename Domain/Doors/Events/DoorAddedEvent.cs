using Domain.Common;

namespace Domain.Doors.Events;

public class DoorAddedEvent : Event
{
    public DoorAddedEvent(Guid officeId, Guid doorId)
    {
        DoorId = doorId;
        AggregateId = officeId;
    }

    public Guid DoorId { get; private set; }
}
