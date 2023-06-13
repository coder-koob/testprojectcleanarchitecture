using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Doors;
using Domain.Doors.Commands;
using Domain.Interfaces;
using Domain.Offices;

namespace Application.Doors.Commands;

public class AddDoorCommandHandler : CommandHandler<AddDoorCommand, Door?>
{
    private readonly IEventSourcedRepository<Office> _repository;

    public AddDoorCommandHandler(IEventSourcedRepository<Office> repository, ICurrentUserService currentUserService)
        : base(currentUserService)
    {
        _repository = repository;
    }

    protected override async Task<Door?> HandleCommand(AddDoorCommand command, CancellationToken cancellationToken)
    {
        var office = await _repository.GetByIdAsync(command.Payload.OfficeId) ?? throw new NotFoundException(nameof(Office), command.Payload.OfficeId);

        var door = office.AddDoor(command);
        await _repository.SaveAsync(office);

        return door;
    }
}