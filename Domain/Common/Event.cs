using MediatR;

namespace Domain.Common;

public class Event : INotification
{
    public Event(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }

    public Guid Id { get; set; }
    public Guid AggregateId { get; private set; }
    public int Version { get; set; }
    public string? Type { get; set; }
}