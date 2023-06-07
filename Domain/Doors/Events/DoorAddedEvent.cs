using Domain.Common;

namespace Domain.Doors.Events;

public class DoorAddedEvent : Event
{
    public string OfficeId { get; }
    public string DoorId { get; }

    public DoorAddedEvent(string officeId, string doorId)
    {
        OfficeId = officeId;
        DoorId = doorId;
    }
}