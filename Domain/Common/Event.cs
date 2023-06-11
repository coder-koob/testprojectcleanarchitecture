using MediatR;

namespace Domain.Common;

public abstract class Event : INotification
{
    public Event(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }

    public Guid Id { get; set; }
    public Guid AggregateId { get; private set; }
    public int Version { get; set; }
    public string? Type { get; set; }
    public DateTimeOffset Timestamp
    {
        get { return new DateTimeOffset(DateTime, Offset); }
        set
        {
            DateTime = value.DateTime;
            Offset = value.Offset;
        }
    }

    public DateTime DateTime { get; set; }
    public TimeSpan Offset { get; set; }
}