using Domain.Common;

namespace Domain.Doors;

public class Door
{
    public Door(Guid doorId)
    {
        DoorId = doorId;
        IsLocked = false;
    }

    public Guid DoorId { get; set; }
    public bool IsLocked { get; private set; }

    public void Lock()
    {
        IsLocked = true;
    }

    public void Unlock()
    {
        IsLocked = false;
    }
}
