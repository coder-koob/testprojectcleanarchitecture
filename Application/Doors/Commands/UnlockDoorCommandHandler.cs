using Application.Common.Services;
using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Doors.Commands;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Application.Doors.Commands;

public class UnlockDoorCommandHandler : CommandHandler<UnlockDoorCommand>
{
    private readonly IEventSourcedRepository<Office> _repository;
    private readonly IClientService _clientService;

    public UnlockDoorCommandHandler(
        IEventSourcedRepository<Office> repository,
        IClientService clientService,
        ICurrentUserService currentUserService)
        : base(currentUserService)
    {
        _repository = repository;
        _clientService = clientService;
    }

    protected override async Task HandleCommand(UnlockDoorCommand command, CancellationToken cancellationToken)
    {
        var office = await _repository.GetByIdAsync(command.Payload.OfficeId) ?? throw new NotFoundException(nameof(Office), command.Payload.OfficeId);;

        var door = office.Doors.First(x => x.DoorId == command.Payload.DoorId);

        if (!_clientService.IsClientAuthorized(Context.ClientId!, door.Scope!))
        {
            throw new ForbiddenAccessException();
        }

        office.UnlockDoor(command);
        await _repository.SaveAsync(office);
    }
}