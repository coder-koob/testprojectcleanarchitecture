using Domain.Common;
using Domain.Doors.Commands;
using Domain.Doors.Events;

namespace Domain.Doors;

public partial class Door : Entity
{
    public Door()
    {
    }

    public Door(Guid officeId, Guid doorId, string name)
    {
        OfficeId = officeId;
        DoorId = doorId;
        Name = name;
        IsLocked = false;
    }

    public Guid OfficeId { get; set; }
    public Guid DoorId { get; set; }
    public string? Name { get; set; }
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
