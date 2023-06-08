namespace Domain.Common;

public abstract class Entity
{
    protected readonly List<Event> _changes = new();

    public static T Rehydrate<T>(IEnumerable<Event> events) where T : Entity, new()
    {
        var instance = new T();
        foreach (var @event in events)
        {
            instance.ApplyEvent(@event);
        }
        return instance;
    }

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

