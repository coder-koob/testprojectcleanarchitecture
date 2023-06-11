using Domain.Common;
using Domain.Doors.Commands;

namespace Domain.Doors.Events;

public class DoorLockedEvent : Event
{
    public DoorLockedEvent(Guid officeId, LockDoorCommand command)
        : base(officeId)
    {
        OfficeId = officeId;
        DoorId = command.Payload.DoorId;
    }

    public Guid DoorId { get; }
    public Guid OfficeId { get; }
}