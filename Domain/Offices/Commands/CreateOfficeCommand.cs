using Domain.Common;
using Domain.Interfaces;
using MediatR;

namespace Domain.Offices.Commands;

public record CreateOfficePayload(string Name);

public class CreateOfficeCommand : Command<CreateOfficePayload, Guid>
{
    public CreateOfficeCommand(CreateOfficePayload payload)
        : base(payload)
    {
    }
}
