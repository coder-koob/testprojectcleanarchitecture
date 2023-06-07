using Domain.Common;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class MongoDbEventStore : IEventStore
{
    private readonly IMongoCollection<Event> _events;

    public MongoDbEventStore(IOptions<MongoDbOptions> mongoDbOptions)
    {
        var client = new MongoClient(mongoDbOptions.Value.ConnectionString);
        var database = client.GetDatabase("eventstore");
        _events = database.GetCollection<Event>("events");
    }

    public async Task<IEnumerable<Event>> GetEventsForAggregate(Guid aggregateId)
    {
        return await _events.Find(e => e.AggregateId == aggregateId)
            .SortBy(e => e.Version)
            .ToListAsync();
    }

    public async Task SaveEventsForAggregate(Guid aggregateId, IEnumerable<Event> events)
    {
        await _events.InsertManyAsync(events);
    }
}