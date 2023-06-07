using Domain.Common;
using Domain.Interfaces;

namespace Infrastructure.Persistence;

public class EventSourcedRepository<T> : IEventSourcedRepository<T> where T : Aggregate, new()
{
    private readonly IEventStore _eventStore;

    public EventSourcedRepository(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var events = await _eventStore.GetEvents(id);
        return Aggregate.Rehydrate<T>(events);
    }

    public async Task SaveAsync(T aggregate)
    {
        var changes = aggregate.GetChanges();

        foreach (var @event in changes)
        {
            await _eventStore.SaveEvent(aggregate.AggregateId, @event);
        }

        aggregate.ClearChanges();
    }
}