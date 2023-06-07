using Domain.Common;

namespace Domain.Interfaces;

public interface IEventStore
{
    Task SaveEvent(string aggregateId, Event @event);
    Task<IEnumerable<Event>> GetEvents(string aggregateId);
}