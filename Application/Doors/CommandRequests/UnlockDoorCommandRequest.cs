using Application.Common.Security;
using Domain.Doors.Commands;
using MediatR;

namespace Application.Doors.CommandRequests;

[Authorize(Scope = Config.UnlockDoorScope)]
public class UnlockDoorCommandRequest : IRequest
{
    public UnlockDoorCommandRequest(Guid officeId, Guid doorId)
    {
        OfficeId = officeId;
        DoorId = doorId;
    }

    public Guid OfficeId { get; private set; }
    public Guid DoorId { get; set; }
}

public class UnlockDoorCommandRequestHandler : IRequestHandler<UnlockDoorCommandRequest>
{
    private readonly IMediator _mediator;

    public UnlockDoorCommandRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UnlockDoorCommandRequest request, CancellationToken cancellationToken)
    {
        var command = new UnlockDoorCommand(new UnlockDoorPayload(request.OfficeId, request.DoorId));

        await _mediator.Send(command, cancellationToken);
    }
}