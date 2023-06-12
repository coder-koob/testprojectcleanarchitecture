using Application.Common.Services;
using Domain.Common.Exceptions;
using Domain.Doors.Commands;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Application.Doors.Commands;

public class UnlockDoorCommandHandler : IRequestHandler<UnlockDoorCommand>
{
    private readonly IEventSourcedRepository<Office> _repository;
    private readonly IClientService _clientService;
    private readonly ICurrentUserService _currentUserService;

    public UnlockDoorCommandHandler(
        IEventSourcedRepository<Office> repository,
        IClientService clientService,
        ICurrentUserService currentUserService)
    {
        _repository = repository;
        _clientService = clientService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UnlockDoorCommand command, CancellationToken cancellationToken)
    {
        var office = await _repository.GetByIdAsync(command.Payload.OfficeId);

        var door = office.Doors.First(x => x.DoorId == command.Payload.DoorId);

        if (!_clientService.IsClientAuthorized(_currentUserService.ClientId!, door.Scope!))
        {
            throw new ForbiddenAccessException();
        }

        if (office is not null)
        {
            office.UnlockDoor(command);
            await _repository.SaveAsync(office);
        }
    }
}