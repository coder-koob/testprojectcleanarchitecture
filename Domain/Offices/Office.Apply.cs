using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Doors;
using Domain.Doors.Events;
using Domain.Offices.Events;

namespace Domain.Offices;

public partial class Office
{
    public override void ApplyEvent(Event @event)
    {
        switch (@event)
        {
            case OfficeCreatedEvent e:
                Id = e.AggregateId;
                Name = e.Name;
                break;

            case DoorAddedEvent e:
                var door = new Door(e.DoorId);
                _doors.Add(door);
                break;

            case DoorLockedEvent e:
                var doorToLock = _doors.First(d => d.Id == e.DoorId);
                doorToLock.Lock();
                break;

            default:
                throw new Exception($"Unhandled event type: {@event.GetType().FullName}");
        }
    }
}