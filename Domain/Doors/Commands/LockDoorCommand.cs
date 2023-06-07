using Domain.Common;
using MediatR;

namespace Domain.Doors.Commands;

public class LockDoorCommand : Command
{   
    public LockDoorCommand(string doorId)
        : base("LockDoor")
    {
        DoorId = doorId;
    }

    public string DoorId { get; private set; }
}

public class LockDoorCommandHandler : IRequestHandler<LockDoorCommand>
{
    public Task Handle(LockDoorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}