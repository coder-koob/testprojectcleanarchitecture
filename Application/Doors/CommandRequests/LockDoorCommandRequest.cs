using MediatR;

namespace Application.Doors.CommandRequests;

public class LockDoorCommandRequest : IRequest
{
    public LockDoorCommandRequest(string doorId)
    {
        DoorId = doorId;
    }

    public string DoorId { get; private set; }
}