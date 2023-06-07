using Domain.Common;
using Domain.Doors;
using Domain.Doors.Events;

namespace Domain.Offices;

public class Office
{
    private readonly List<Door> _doors = new();
    private readonly List<Event> _changes = new();

    public string OfficeId { get; private set; }
    public IReadOnlyCollection<Door> Doors => _doors.AsReadOnly();

    public Office(string officeId)
    {
        OfficeId = officeId;
    }

    public void AddDoor(string doorId)
    {
        if (_doors.Any(d => d.DoorId == doorId))
        {
            throw new Exception($"Door with id {doorId} already exists in the office.");
        }

        var door = new Door(doorId);
        _doors.Add(door);

        var @event = new DoorAddedEvent(OfficeId, doorId);
        _changes.Add(@event);
    }

    public void LockDoor(string doorId)
    {
        var door = _doors.FirstOrDefault(d => d.DoorId == doorId);

        if (door == null)
        {
            throw new Exception($"Door with id {doorId} does not exist in the office.");
        }

        if (door.IsLocked)
        {
            throw new Exception($"Door with id {doorId} is already locked.");
        }

        door.Lock();

        var @event = new DoorLockedEvent(OfficeId, doorId);
        _changes.Add(@event);
    }

    public IEnumerable<Event> GetChanges()
    {
        return _changes.AsReadOnly();
    }

    public void ClearChanges()
    {
        _changes.Clear();
    }
}

