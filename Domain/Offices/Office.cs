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

    internal Office(Guid officeId)
    {
        OfficeId = officeId;
    }

    public Guid OfficeId;
    public string? Name;
    public IReadOnlyCollection<Door> Doors => _doors.AsReadOnly();

    public static Office Create(Guid id, CreateOfficeCommand command)
    {
        var office = new Office(id)
        {
            Name = command.Payload.Name
        };

        office.ApplyChange(new OfficeCreatedEvent(id, command));

        return office;
    }

    public void AddDoor(AddDoorCommand command)
    {
        if (_doors.Any(d => d.DoorId == command.Payload.DoorId))
        {
            throw new Exception($"Door with id {command.Payload.DoorId} already exists in the office.");
        }

        var doorAddedEvent = new DoorAddedEvent(OfficeId, command);
        ApplyChange(doorAddedEvent);
    }

    public void LockDoor(Guid doorId)
    {
        var door = _doors.FirstOrDefault(d => d.DoorId == doorId) ?? throw new Exception($"Door with id {doorId} does not exist in the office.");
        if (door.IsLocked)
        {
            throw new Exception($"Door with id {doorId} is already locked.");
        }

        var doorLockedEvent = new DoorLockedEvent(OfficeId, doorId);
        ApplyChange(doorLockedEvent);
    }
}

