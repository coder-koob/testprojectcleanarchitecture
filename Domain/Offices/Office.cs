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

    protected Office()
    {
    }

    internal Office(Guid id)
    {
        Id = id;
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
        if (_doors.Any(d => d.Id == doorId))
        {
            throw new Exception($"Door with id {doorId} already exists in the office.");
        }

        var doorAddedEvent = new DoorAddedEvent(Id, doorId);
        ApplyChange(doorAddedEvent);
    }

    public void LockDoor(Guid doorId)
    {
        var door = _doors.FirstOrDefault(d => d.Id == doorId) ?? throw new Exception($"Door with id {doorId} does not exist in the office.");
        if (door.IsLocked)
        {
            throw new Exception($"Door with id {doorId} is already locked.");
        }

        var doorLockedEvent = new DoorLockedEvent(Id, doorId);
        ApplyChange(doorLockedEvent);
    }
}

