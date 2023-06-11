using Domain.Common;
using Domain.Interfaces;
using Domain.Offices;
using Domain.Offices.Commands;
using MediatR;

namespace Application.Offices.Commands;

public class CreateOfficeCommandHandler : CommandHandler<CreateOfficeCommand, Office>
{
    private readonly IEventSourcedRepository<Office> _officeRepository;

    public CreateOfficeCommandHandler(IEventSourcedRepository<Office> officeRepository)
    {
        _officeRepository = officeRepository;
    }

    protected override async Task<Office> HandleCommand(CreateOfficeCommand command, CancellationToken cancellationToken)
    {
        var office = Office.Create(Guid.NewGuid(), command);
        await _officeRepository.SaveAsync(office);

        return office;
    }
}