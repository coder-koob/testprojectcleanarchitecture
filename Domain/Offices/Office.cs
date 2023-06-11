using Domain.Common;
using Domain.Doors;
using Domain.Doors.Commands;
using Domain.Doors.Events;
using Domain.Offices.Commands;
using Domain.Offices.Events;

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

    public void AddDoor(AddDoorCommand command)
    {
        if (_doors.Any(d => d.Name == command.Payload.Name))
        {
            throw new Exception($"Door with name {command.Payload.Name} already exists in the office.");
        }

        var @event = new DoorAddedEvent(OfficeId, command);
        ApplyChange(@event);
    }

    public void LockDoor(LockDoorCommand command)
    {
        var doorId = command.Payload.DoorId;
        var door = _doors.FirstOrDefault(d => d.DoorId == doorId) ?? throw new Exception($"Door with id {doorId} does not exist in the office.");
        if (door.IsLocked)
        {
            throw new Exception($"Door with id {doorId} is already locked.");
        }

        var @event = new DoorLockedEvent(OfficeId, command);
        ApplyChange(@event);
    }

    public void UnlockDoor(UnlockDoorCommand command)
    {
        var doorId = command.Payload.DoorId;
        var door = _doors.FirstOrDefault(d => d.DoorId == doorId) ?? throw new Exception($"Door with id {doorId} does not exist in the office.");
        if (door.IsLocked)
        {
            throw new Exception($"Door with id {doorId} is already locked.");
        }

        var @event = new DoorUnlockedEvent(OfficeId, command);
        ApplyChange(@event);
    }
}

