using Domain.Common;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Domain.Doors.Commands;

public record AddDoorPayload(Guid OfficeId, Guid DoorId);

public class AddDoorCommand : Command<AddDoorPayload>
{
    public AddDoorCommand(AddDoorPayload payload)
        : base("AddDoor", payload)
    {
    }
}

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
            office.AddDoor(command.Payload.DoorId);
            await _repository.SaveAsync(office);
        }
    }
}