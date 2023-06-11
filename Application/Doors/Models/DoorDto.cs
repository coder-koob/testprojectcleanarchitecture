using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Doors.Models;

public class DoorDto
{
    public DoorDto()
    {
    }

    public DoorDto(Guid officeId, Guid doorId)
    {
        OfficeId = officeId;
        DoorId = doorId;
    }

    public DoorDto(Guid officeId, Guid doorId, string? name)
    {
        OfficeId = officeId;
        DoorId = doorId;
        Name = name;
    }

    public DoorDto(Guid officeId, Guid doorId, string? name, bool isLocked)
    {
        OfficeId = officeId;
        DoorId = doorId;
        Name = name;
        IsLocked = isLocked;
    }

    public Guid OfficeId { get; set; }
    public Guid DoorId { get; set; }
    public string? Name { get; set; }
    public bool IsLocked { get; set; }
}