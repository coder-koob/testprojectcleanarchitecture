using Domain.Common;

namespace Domain.Doors.Events;

public class DoorLockedEvent : Event
{
    public string OfficeId { get; }
    public string DoorId { get; }

    public DoorLockedEvent(string officeId, string doorId)
    {
        OfficeId = officeId;
        DoorId = doorId;
    }
}