using Domain.Doors.Commands;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Application.Doors.Commands;

public class UnlockDoorCommandHandler : IRequestHandler<UnlockDoorCommand>
{
    private readonly IEventSourcedRepository<Office> _repository;

    public UnlockDoorCommandHandler(IEventSourcedRepository<Office> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UnlockDoorCommand command, CancellationToken cancellationToken)
    {
        var office = await _repository.GetByIdAsync(command.Payload.OfficeId);

        if (office is not null)
        {
            office.UnlockDoor(command);
            await _repository.SaveAsync(office);
        }
    }
}