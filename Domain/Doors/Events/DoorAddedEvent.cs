using System.Text.Json;
using Domain.Common;
using Domain.Doors.Commands;

namespace Domain.Doors.Events;

public class DoorAddedEvent : Event
{
    public DoorAddedEvent(Guid officeId, AddDoorCommand command)
        : base (officeId)
    {
        DoorId = command.Payload.DoorId;
        OfficeId = officeId;
    }
    
    public Guid OfficeId { get; private set; }
    public Guid DoorId { get; private set; }
}
