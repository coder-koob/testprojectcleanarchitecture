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

public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Guid>
{
    private readonly IEventSourcedRepository<Office> _officeRepository;

    public CreateOfficeCommandHandler(IEventSourcedRepository<Office> officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<Guid> Handle(CreateOfficeCommand command, CancellationToken cancellationToken)
    {
        var office = Office.Create(Guid.NewGuid(), command);
        await _officeRepository.SaveAsync(office.OfficeId, office);

        return office.OfficeId;
    }
}
