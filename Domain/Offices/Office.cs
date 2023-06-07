using Domain.Common;
using Domain.Doors;
using Domain.Doors.Events;
using Domain.Offices.Events;

namespace Domain.Offices;

public partial class Office : Aggregate
{
    private readonly List<Door> _doors = new();

    public string? Name;
    public IReadOnlyCollection<Door> Doors => _doors.AsReadOnly();

    public Office()
    {
    }

    internal Office(Guid id)
    {
        AggregateId = id;
    }

    public static Office Create(Guid id, string name)
    {
        var office = new Office(id)
        {
            Name = name
        };

        office.ApplyChange(new OfficeCreatedEvent(id, name));

        return office;
    }

    public void AddDoor(Guid doorId)
    {
        if (_doors.Any(d => d.DoorId == doorId))
        {
            throw new Exception($"Door with id {doorId} already exists in the office.");
        }

        var doorAddedEvent = new DoorAddedEvent(AggregateId, doorId);
        ApplyChange(doorAddedEvent);
    }

    public void LockDoor(Guid doorId)
    {
        var door = _doors.FirstOrDefault(d => d.DoorId == doorId) ?? throw new Exception($"Door with id {doorId} does not exist in the office.");
        if (door.IsLocked)
        {
            throw new Exception($"Door with id {doorId} is already locked.");
        }

        var doorLockedEvent = new DoorLockedEvent(AggregateId, doorId);
        ApplyChange(doorLockedEvent);
    }
}

