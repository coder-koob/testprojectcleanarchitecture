using Domain.Common;
using Domain.Doors.Commands;

namespace Domain.Doors.Events;

public class DoorUnlockedEvent : Event
{
    public DoorUnlockedEvent(Guid officeId, UnlockDoorCommand command)
        : base(officeId, command.Context)
    {
        OfficeId = officeId;
        DoorId = command.Payload.DoorId;
    }

    public Guid OfficeId { get; private set; }
    public Guid DoorId { get; private set; }
}