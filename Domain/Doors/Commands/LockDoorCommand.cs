using Domain.Common;
using Domain.Doors.Events;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Domain.Doors.Commands;

public record LockDoorPayload(Guid OfficeId, Guid DoorId);

public class LockDoorCommand : Command<LockDoorPayload>
{
    public LockDoorCommand(LockDoorPayload payload)
        : base(payload)
    {
    }
}

public class LockDoorCommandHandler : IRequestHandler<LockDoorCommand>
{
    private readonly IEventSourcedRepository<Office> _repository;

    public LockDoorCommandHandler(IEventSourcedRepository<Office> repository)
    {
        _repository = repository;
    }

    public async Task Handle(LockDoorCommand command, CancellationToken cancellationToken)
    {
        var office = await _repository.GetByIdAsync(command.Payload.OfficeId);

        if (office is not null)
        {
            office.LockDoor(command.Payload.DoorId);
            await _repository.SaveAsync(office.OfficeId, office);
        }
    }
}