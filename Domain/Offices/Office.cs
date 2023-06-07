using Domain.Common;
using Domain.Doors;
using Domain.Doors.Events;
using Domain.Offices.Events;

namespace Domain.Offices;

public class Office : Aggregate
{
    private readonly List<Door> _doors = new();

    public string? Name;
    public IReadOnlyCollection<Door> Doors => _doors.AsReadOnly();

    public Office(Guid id, string name)
    {
        Id = id;
        Name = name;
        Apply(new OfficeCreatedEvent(id, name));
    }

    public override void Apply(Event @event)
    {
        switch (@event)
        {
            case OfficeCreatedEvent officeCreated:
                Name = officeCreated.Name;
                break;
                
            case DoorAddedEvent doorAdded:
                var door = new Door(doorAdded.Id);
                _doors.Add(door);
                break;

            case DoorLockedEvent doorLocked:
                var doorToLock = _doors.First(d => d.Id == doorLocked.Id);
                doorToLock.Lock();
                break;

            default:
                throw new Exception($"Unhandled event type: {@event.GetType().FullName}");
        }

        base.Apply(@event);
    }

    public void AddDoor(Guid doorId)
    {
        if (_doors.Any(d => d.Id == doorId))
        {
            throw new Exception($"Door with id {doorId} already exists in the office.");
        }

        var door = new Door(doorId);
        _doors.Add(door);
    }

    public void LockDoor(Guid doorId)
    {
        var door = _doors.FirstOrDefault(d => d.Id == doorId) ?? throw new Exception($"Door with id {doorId} does not exist in the office.");
        if (door.IsLocked)
        {
            throw new Exception($"Door with id {doorId} is already locked.");
        }

        door.Lock();
    }
}

