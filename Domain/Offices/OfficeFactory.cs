using Domain.Common;
using Domain.Doors.Events;
using Domain.Offices.Events;

namespace Domain.Offices;

public class OfficeFactory : IOfficeFactory
{
    public Office Create(Guid officeId, string name)
    {
        return new Office(officeId, name);
    }

    public Office? Rehydrate(IEnumerable<Event> events)
    {
        Office? office = null;

        foreach (var @event in events)
        {
            switch (@event)
            {
                case OfficeCreatedEvent officeCreated:
                    office = Create(officeCreated.Id, officeCreated.Name);
                    break;

                case DoorAddedEvent doorAdded:
                    office?.Apply(doorAdded);
                    break;

                case DoorLockedEvent doorLocked:
                    office?.Apply(doorLocked);
                    break;

                // handle other event types...
            }
        }

        return office;
    }
}
