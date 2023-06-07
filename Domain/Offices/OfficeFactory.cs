using Domain.Common;
using Domain.Doors.Events;

namespace Domain.Offices;

public class OfficeFactory : IOfficeFactory
{
    public Office Create(string officeId)
    {
        return new Office(officeId);
    }

    public Office? Rehydrate(IEnumerable<Event> events)
    {
        Office? office = null;

        foreach (var @event in events)
        {
            switch (@event)
            {
                // case OfficeCreatedEvent officeCreated:
                //     office = Create(officeCreated.OfficeId);
                //     break;

                case DoorAddedEvent doorAdded:
                    office?.AddDoor(doorAdded.DoorId);
                    break;

                case DoorLockedEvent doorLocked:
                    office?.LockDoor(doorLocked.DoorId);
                    break;

                // handle other event types...
            }
        }

        return office;
    }
}
