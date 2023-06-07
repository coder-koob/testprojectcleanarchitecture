using Domain.Doors.Commands;
using MediatR;

namespace Application.Doors.CommandRequests;

public class AddDoorCommandRequest : IRequest
{
    public AddDoorCommandRequest(Guid officeId, Guid doorId)
    {
        OfficeId = officeId;
        DoorId = doorId;
    }

    public Guid OfficeId { get; private set; }
    public Guid DoorId { get; private set; }
}

public class AddDoorCommandRequestHandler : IRequestHandler<AddDoorCommandRequest>
{
    private readonly IMediator _mediator;

    public AddDoorCommandRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(AddDoorCommandRequest request, CancellationToken cancellationToken)
    {
        var command = new AddDoorCommand(new AddDoorPayload(request.OfficeId, request.DoorId));

        await _mediator.Send(command, cancellationToken);
    }
}