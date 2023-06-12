using Application.Common.Security;
using Application.Doors.Models;
using Domain.Doors.Commands;
using MediatR;

namespace Application.Doors.CommandRequests;

[Authorize(Scope = Config.AddDoorScope)]
public class AddDoorCommandRequest : IRequest<DoorAddedDto>
{
    public AddDoorCommandRequest(Guid officeId, string name, string scope)
    {
        OfficeId = officeId;
        Name = name;
        Scope = scope;
    }

    public Guid OfficeId { get; private set; }
    public string Name { get; private set; }
    public string Scope { get; set; }
}

public class AddDoorCommandRequestHandler : IRequestHandler<AddDoorCommandRequest, DoorAddedDto>
{
    private readonly IMediator _mediator;

    public AddDoorCommandRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<DoorAddedDto> Handle(AddDoorCommandRequest request, CancellationToken cancellationToken)
    {
        var command = new AddDoorCommand(new AddDoorPayload(request.OfficeId, Guid.NewGuid(), request.Name, request.Scope));

        var door = await _mediator.Send(command, cancellationToken);

        var response = new DoorAddedDto(door.OfficeId, door.DoorId);

        return response;
    }
}