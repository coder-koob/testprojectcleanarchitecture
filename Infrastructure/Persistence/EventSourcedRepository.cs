using Domain.Common;
using Domain.Interfaces;

namespace Infrastructure.Persistence;

public class EventSourcedRepository<T> : IEventSourcedRepository<T> where T : Entity, new()
{
    private readonly IEventStore _eventStore;

    public EventSourcedRepository(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var events = await _eventStore.GetEvents(id);
        return Entity.Rehydrate<T>(events);
    }

    public async Task SaveAsync(Guid aggregateId, T entity)
    {
        var changes = entity.GetChanges();

        foreach (var @event in changes)
        {
            await _eventStore.SaveEvent(aggregateId, @event);
        }

        entity.ClearChanges();
    }
}