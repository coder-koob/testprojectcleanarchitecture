using Domain.Doors;

namespace Domain.Offices;

public class Office
{
    public string Id { get; private set; }
    public List<Door> Doors { get; private set; }

    public Office(string id, List<Door> doors)
    {
        Id = id;
        Doors = doors;
    }

    public void LockDoor(string doorId)
    {
        var door = Doors.Find(d => d.Id == doorId);
        if (door != null && !door.IsLocked)
        {
            door.Lock();
            // raise DoorLocked event
        }
    }

    public void UnlockDoor(string doorId)
    {
        var door = Doors.Find(d => d.Id == doorId);
        if (door != null && door.IsLocked)
        {
            door.Unlock();
            // raise DoorUnlocked event
        }
    }
}
