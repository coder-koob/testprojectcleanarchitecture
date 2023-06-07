using Domain.Common;

namespace Domain.Doors.Commands;

public class UnlockDoorCommand : Command
{
    public UnlockDoorCommand(string topic)
        : base(topic)
    {
    }
}