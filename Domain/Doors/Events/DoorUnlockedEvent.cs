using Domain.Common;
using Domain.Doors.Commands;

namespace Domain.Doors.Events;

public class DoorUnlockedEvent : Event
{
    public DoorUnlockedEvent(Guid officeId, UnlockDoorCommand command)
        : base(officeId)
    {
        OfficeId = officeId;
        DoorId = command.Payload.DoorId;
    }

    public Guid DoorId { get; }
    public Guid OfficeId { get; }
}