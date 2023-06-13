using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Doors.Models;

public class DoorEventDto
{
    public DoorEventDto(string @event, string clientId, DateTimeOffset timestamp)
    {
        Event = @event;
        ClientId = clientId;
        Timestamp = timestamp;
    }

    public string Event { get; set; } = null!;

    public string? ClientId { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}