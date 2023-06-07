using Domain.Common;

namespace Domain.Doors.Commands;

public class CreateDoorCommand : Command
{
    public CreateDoorCommand(string topic)
        : base(topic)
    {
    }
}