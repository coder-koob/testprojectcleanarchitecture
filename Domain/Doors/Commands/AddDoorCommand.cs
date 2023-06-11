using Domain.Common;

namespace Domain.Doors.Commands;

public record AddDoorPayload(Guid OfficeId, Guid DoorId);

public class AddDoorCommand : Command<AddDoorPayload>
{
    public AddDoorCommand(AddDoorPayload payload)
        : base(payload)
    {
    }
}