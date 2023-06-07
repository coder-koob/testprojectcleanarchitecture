using Domain.Common;

namespace Domain.Interfaces;

public interface IEventStore
{
    Task<IEnumerable<Event>> GetEventsForAggregate(Guid aggregateId);
    Task SaveEventsForAggregate(Guid aggregateId, IEnumerable<Event> events);
}