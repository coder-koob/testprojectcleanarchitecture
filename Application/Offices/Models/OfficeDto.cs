using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Doors.Models;

namespace Application.Offices.Models;

public class OfficeDto
{
    public Guid OfficeId { get; set; }

    public string? Name { get; set; }

    public IList<DoorDto> Doors { get; set; } = new List<DoorDto>();
}