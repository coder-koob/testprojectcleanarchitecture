using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Doors.Commands;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Application.Doors.Commands;

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
            await _repository.SaveAsync(office);
        }
    }
}