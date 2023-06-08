using System.Text.Json;
using Domain.Common;
using Domain.Offices.Commands;

namespace Domain.Offices.Events;

public class OfficeCreatedEvent : Event
{
    public OfficeCreatedEvent(Guid officeId, CreateOfficeCommand command)
        : base(officeId)
    {
        OfficeId = officeId;
        Payload = JsonSerializer.Serialize(command.Payload);
    }

    public Guid OfficeId { get; private set; }
}