using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Doors;
using Domain.Doors.Events;
using Domain.Offices.Commands;
using Domain.Offices.Events;

namespace Domain.Offices;

public partial class Office
{
    public override void ApplyEvent(Event @event)
    {
        switch (@event)
        {
            case OfficeCreatedEvent e:
                OfficeId = e.OfficeId;
                Name = JsonSerializer.Deserialize<CreateOfficePayload>(e.Payload)?.Name;
                break;

            case DoorAddedEvent e:
                var door = new Door(e.DoorId);
                _doors.Add(door);
                break;

            case DoorLockedEvent e:
                var doorToLock = _doors.First(d => d.DoorId == e.DoorId);
                doorToLock.Lock();
                break;

            default:
                throw new Exception($"Unhandled event type: {@event.GetType().FullName}");
        }
    }
}