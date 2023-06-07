using Domain.Common;

namespace Domain.Doors.Commands;

public class UpdateDoorCommand : Command
{
    public UpdateDoorCommand(string topic)
        : base(topic)
    {
    }
}