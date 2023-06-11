using Domain.Common;

namespace Domain.Offices.Commands;

public record CreateOfficePayload(string Name);

public class CreateOfficeCommand : Command<CreateOfficePayload, Guid>
{
    public CreateOfficeCommand(CreateOfficePayload payload)
        : base(payload)
    {
    }
}
