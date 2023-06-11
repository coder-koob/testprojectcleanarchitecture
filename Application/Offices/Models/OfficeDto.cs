using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Doors.Models;

namespace Application.Offices.Models;

public class OfficeDto
{
    public OfficeDto(Guid officeId, string? name, IList<DoorDto> doors)
    {
        OfficeId = officeId;
        Name = name;
        Doors = doors;
    }

    public Guid OfficeId { get; set; }

    public string? Name { get; set; }

    public IList<DoorDto> Doors { get; set; } = new List<DoorDto>();
}