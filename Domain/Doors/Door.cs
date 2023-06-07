using Domain.Common;

namespace Domain.Doors;

public class Door : Entity
{
    public bool IsLocked { get; private set; }

    public Door(Guid doorId)
        : base(doorId)
    {
        IsLocked = false;
    }

    public void Lock()
    {
        IsLocked = true;
    }

    public void Unlock()
    {
        IsLocked = false;
    }
}
