using Application.Common.Security;
using Application.Common.Services;
using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Doors.Commands;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Application.Doors.Commands;

public class LockDoorCommandHandler : CommandHandler<LockDoorCommand>
{
    private readonly IEventSourcedRepository<Office> _repository;
    private readonly IClientService _clientService;

    public LockDoorCommandHandler(
        IEventSourcedRepository<Office> repository,
        IClientService clientService,
        ICurrentUserService currentUserService)
        : base(currentUserService)
    {
        _clientService = clientService;
        _repository = repository;
    }

    protected override async Task HandleCommand(LockDoorCommand command, CancellationToken cancellationToken)
    {
        var office = await _repository.GetByIdAsync(command.Payload.OfficeId) ?? throw new NotFoundException(nameof(Office), command.Payload.OfficeId);;

        var door = office.Doors.First(x => x.DoorId == command.Payload.DoorId);

        if (!_clientService.IsClientAuthorized(Context.ClientId!, door.Scope!))
        {
            throw new ForbiddenAccessException();
        }

        office.LockDoor(command);
        await _repository.SaveAsync(office);
    }
}