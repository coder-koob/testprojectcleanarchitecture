using Domain.Common;

namespace Domain.Interfaces;

public interface IEventStore
{
    Task SaveEvent(Guid aggregateId, Event @event);
    Task<IEnumerable<Event>> GetEvents(Guid aggregateId);
}