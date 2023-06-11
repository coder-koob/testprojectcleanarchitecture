using MediatR;

namespace Application.Doors.CommandRequests;

public class UnlockDoorCommandRequest : IRequestHandler<AddDoorCommandRequest>
{
    public Task Handle(AddDoorCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}