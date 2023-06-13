using System.Text.Json;
using Domain.Common;
using Domain.Doors.Commands;

namespace Domain.Doors.Events;

public class DoorAddedEvent : Event
{
    public DoorAddedEvent(Guid officeId, AddDoorCommand command)
        : base(officeId, command.Context)
    {
        DoorId = command.Payload.DoorId;
        Name = command.Payload.Name;
        OfficeId = officeId;
        Scope = command.Payload.Scope;
    }

    public Guid OfficeId { get; private set; }
    public Guid DoorId { get; private set; }
    public string Name { get; private set; }
    public string Scope { get; private set; }
}
