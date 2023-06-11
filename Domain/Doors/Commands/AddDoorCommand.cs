using Domain.Common;

namespace Domain.Doors.Commands;

public record AddDoorPayload(Guid OfficeId, Guid DoorId, string Name);

public class AddDoorCommand : Command<AddDoorPayload, Door>
{
    public AddDoorCommand(AddDoorPayload payload)
        : base(payload)
    {
    }
}