using Domain.Doors.Commands;
using MediatR;

namespace Application.Doors.CommandRequests;

public class LockDoorCommandRequest : IRequest
{
    public LockDoorCommandRequest(string officeId, string doorId)
    {
        OfficeId = officeId;
        DoorId = doorId;
    }

    public string OfficeId { get; private set; }
    public string DoorId { get; private set; }
}

public class LockDoorCommandRequestHandler : IRequestHandler<LockDoorCommandRequest>
{
    private readonly IMediator _mediator;

    public LockDoorCommandRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(LockDoorCommandRequest request, CancellationToken cancellationToken)
    {
        var command = new LockDoorCommand(new LockDoorPayload(request.OfficeId, request.DoorId));

        await _mediator.Send(command, cancellationToken);
    }
}