using Domain.Doors.Commands;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Application.Doors.Commands;

public class AddDoorCommandHandler : IRequestHandler<AddDoorCommand>
{
    private readonly IEventSourcedRepository<Office> _repository;

    public AddDoorCommandHandler(IEventSourcedRepository<Office> repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddDoorCommand command, CancellationToken cancellationToken)
    {
        var office = await _repository.GetByIdAsync(command.Payload.OfficeId);

        if (office is not null)
        {
            office.AddDoor(command);
            await _repository.SaveAsync(office);
        }
    }
}