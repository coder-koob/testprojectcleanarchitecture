using Domain.Interfaces;
using Domain.Offices;
using Domain.Offices.Commands;
using MediatR;

namespace Application.Offices.Commands;

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
        await _officeRepository.SaveAsync(office);

        return office.OfficeId;
    }
}