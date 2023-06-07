using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Offices.Commands;
using MediatR;

namespace Application.Offices.CommandRequests;

public class CreateOfficeCommandRequest : IRequest<Guid>
{
    public CreateOfficeCommandRequest(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}

public class CreateOfficeCommandRequestHandler : IRequestHandler<CreateOfficeCommandRequest, Guid>
{
    private readonly IMediator _mediator;

    public CreateOfficeCommandRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateOfficeCommandRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateOfficeCommand(new CreateOfficePayload(request.Name));

        return await _mediator.Send(command, cancellationToken);
    }
}