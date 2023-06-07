using Application.Doors.CommandRequests;
using Domain.Doors.Commands;
using MediatR;

namespace Application.Doors.Commands;

public class LockDoorCommandRequestHandler : IRequestHandler<LockDoorCommandRequest>
{
    private readonly IMediator _mediator;

    public LockDoorCommandRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(LockDoorCommandRequest request, CancellationToken cancellationToken)
    {
        var command = new LockDoorCommand
        {
            DoorId = request.DoorId,
        };

        await _mediator.Send(command, cancellationToken);
    }
}