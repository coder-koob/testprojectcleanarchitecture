using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Doors;
using Domain.Doors.Commands;
using Domain.Doors.Events;
using Domain.Offices.Commands;
using Domain.Offices.Events;
using FluentValidation.Results;

namespace Domain.Offices;

public partial class Office : Entity
{
    private readonly List<Door> _doors = new();

    public Office()
    {
    }

    public Office(Guid officeId, string name)
    {
        OfficeId = officeId;
        Name = name;
    }

    public Office(Guid officeId, string name, List<Door> doors)
    {
        OfficeId = officeId;
        Name = name;
        _doors = doors;
    }

    public Guid OfficeId;
    public string? Name;
    public IReadOnlyCollection<Door> Doors => _doors.AsReadOnly();

    public static Office Create(Guid id, CreateOfficeCommand command)
    {
        var office = new Office(id, command.Payload.Name);

        var @event = new OfficeCreatedEvent(id, command);
        office.ApplyChange(@event);

        return office;
    }

    public Door AddDoor(AddDoorCommand command)
    {
        if (_doors.Any(d => d.Name == command.Payload.Name))
        {
            throw new ValidationException(nameof(AddDoor), $"Door with name {command.Payload.Name} already exists in the office.");
        }

        var @event = new DoorAddedEvent(OfficeId, command);
        ApplyChange(@event);

        return _doors.First(x => x.DoorId == command.Payload.DoorId);
    }

    public void LockDoor(LockDoorCommand command)
    {
        var doorId = command.Payload.DoorId;
        var door = _doors.FirstOrDefault(d => d.DoorId == doorId) ?? throw new NotFoundException($"Door with id {doorId} does not exist in the office.");
        if (door.IsLocked)
        {
            throw new ValidationException(nameof(LockDoor), $"Door with id {doorId} is already locked.");
        }

        var @event = new DoorLockedEvent(OfficeId, command);
        ApplyChange(@event);
    }

    public void UnlockDoor(UnlockDoorCommand command)
    {
        var doorId = command.Payload.DoorId;
        var door = _doors.FirstOrDefault(d => d.DoorId == doorId) ?? throw new NotFoundException($"Door with id {doorId} does not exist in the office.");
        if (!door.IsLocked)
        {
            throw new ValidationException(nameof(UnlockDoor), $"Door with id {doorId} is already unlocked.");
        }

        var @event = new DoorUnlockedEvent(OfficeId, command);
        ApplyChange(@event);
    }
}

