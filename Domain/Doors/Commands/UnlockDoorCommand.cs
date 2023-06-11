using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Doors.Commands;

public record UnlockDoorPayload(Guid OfficeId, Guid DoorId);

public class UnlockDoorCommand : Command<UnlockDoorPayload>
{
    public UnlockDoorCommand(UnlockDoorPayload payload)
        : base(payload)
    {
    }
}