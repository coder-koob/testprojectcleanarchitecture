namespace Domain.Common;

public class Event
{
    public Guid Id { get; set; }
    public string AggregateId { get; set; }
    public int Version { get; set; }
    public string? Type { get; set; }
    public string? Payload { get; set; }
}