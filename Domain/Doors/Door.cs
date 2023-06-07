using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Doors;

public class Door
{
    public string DoorId { get; private set; }
    public bool IsLocked { get; private set; }

    public Door(string doorId)
    {
        DoorId = doorId;
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
