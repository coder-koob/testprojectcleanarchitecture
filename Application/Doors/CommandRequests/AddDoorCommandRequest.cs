using Application.Doors.Models;
using Domain.Doors.Commands;
using MediatR;

namespace Application.Doors.CommandRequests;

public class AddDoorCommandRequest : IRequest<DoorAddedDto>
{
    public AddDoorCommandRequest(Guid officeId, string name)
    {
        OfficeId = officeId;
        Name = name;
    }

    public Guid OfficeId { get; private set; }
    public string Name { get; private set; }
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
        var command = new AddDoorCommand(new AddDoorPayload(request.OfficeId, Guid.NewGuid(), request.Name));

        var door = await _mediator.Send(command, cancellationToken);

        var response = new DoorAddedDto(door.OfficeId, door.DoorId);

        return response;
    }
}