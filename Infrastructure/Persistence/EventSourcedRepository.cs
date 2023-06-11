using Domain.Common;
using Domain.Interfaces;
using MediatR;

namespace Infrastructure.Persistence;

public class EventSourcedRepository<T> : IEventSourcedRepository<T> where T : Entity, new()
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;

    public EventSourcedRepository(IEventStore eventStore, IMediator mediator)
    {
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task<T> GetByIdAsync(Guid aggregateId)
    {
        var events = await _eventStore.GetEvents(aggregateId);
        return Entity.Rehydrate<T>(events);
    }

    public async Task SaveAsync(T entity)
    {
        // var latestVersion = await _eventStore.GetLatestVersionAsync(entity.AggregateId);
        // if (entity.Version != latestVersion + 1)
        // {
        //     throw new InvalidOperationException($"Concurrency conflict in aggregate {entity.AggregateId}");
        // }

        var changes = entity.GetChanges();

        foreach (var @event in changes)
        {
            await _eventStore.SaveEvent(@event);
            await _mediator.Publish(@event);
        }

        entity.ClearChanges();
    }
}