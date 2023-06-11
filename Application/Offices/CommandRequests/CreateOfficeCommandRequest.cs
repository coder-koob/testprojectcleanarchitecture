using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Offices.Models;
using Domain.Offices.Commands;
using MediatR;

namespace Application.Offices.CommandRequests;

public class CreateOfficeCommandRequest : IRequest<OfficeCreatedDto>
{
    public CreateOfficeCommandRequest(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}

public class CreateOfficeCommandRequestHandler : IRequestHandler<CreateOfficeCommandRequest, OfficeCreatedDto>
{
    private readonly IMediator _mediator;

    public CreateOfficeCommandRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OfficeCreatedDto> Handle(CreateOfficeCommandRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateOfficeCommand(new CreateOfficePayload(request.Name));

        var office = await _mediator.Send(command, cancellationToken);

        var response = new OfficeCreatedDto
        {
            OfficeId = office.OfficeId,
        };

        return response;
    }
}