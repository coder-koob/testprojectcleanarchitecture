using Domain.Common;

namespace Domain.Doors.Commands;

public record LockDoorPayload(Guid OfficeId, Guid DoorId);

public class LockDoorCommand : Command<LockDoorPayload>
{
    public LockDoorCommand(LockDoorPayload payload)
        : base(payload)
    {
    }
}