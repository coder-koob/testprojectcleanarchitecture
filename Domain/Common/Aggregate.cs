using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Common;

public abstract class Aggregate
{
    protected readonly List<Event> _changes = new();

    public Guid Id { get; set; }

    public virtual void Apply(Event @event)
    {
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