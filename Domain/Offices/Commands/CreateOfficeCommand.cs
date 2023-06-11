using Domain.Common;

namespace Domain.Offices.Commands;

public record CreateOfficePayload(string Name);

public class CreateOfficeCommand : Command<CreateOfficePayload, Office>
{
    public CreateOfficeCommand(CreateOfficePayload payload)
        : base(payload)
    {
    }
}
