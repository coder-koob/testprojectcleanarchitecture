using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Doors.Models;

public class DoorEventDto
{
    public DoorEventDto(string @event, DateTimeOffset timestamp)
    {
        Event = @event;
        Timestamp = timestamp;
    }

    public string Event { get; set; } = null!;

    public DateTimeOffset Timestamp { get; set; }
}