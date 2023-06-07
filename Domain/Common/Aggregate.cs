namespace Domain.Common;

public abstract class Aggregate
{
    protected readonly List<Event> _changes = new();

    public Guid Id { get; set; }

    public abstract void ApplyEvent(Event @event);
    
    protected void ApplyChange(Event @event)
    {
        ApplyEvent(@event);
        _changes.Add(@event);
    }

    public IEnumerable<Event> GetChanges()
    {
        return _changes.AsReadOnly();
    }

    public void ClearChanges()
    {
        _changes.Clear();
    }
}
