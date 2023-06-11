using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Doors.Events;

namespace Domain.Doors;

public partial class Door
{
    public override void ApplyEvent(Event @event)
    {
        switch (@event)
        {
            case DoorAddedEvent e:
                OfficeId = e.OfficeId;
                DoorId = e.DoorId;
                Name = e.Name;
                break;

            case DoorLockedEvent:
                Lock();
                break;

            case DoorUnlockedEvent:
                Unlock();
                break;

            default:
                throw new Exception($"Unhandled event type: {@event.GetType().FullName}");
        }
    }
}