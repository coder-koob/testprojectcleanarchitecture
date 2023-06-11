using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MongoDbEventStore : IEventStore
{
    private readonly IMongoCollection<Event> _events;

    public MongoDbEventStore(IOptions<MongoDbOptions> mongoDbOptions)
    {
        var client = new MongoClient(mongoDbOptions.Value.ConnectionString);
        var database = client.GetDatabase(mongoDbOptions.Value.Database);
        _events = database.GetCollection<Event>(mongoDbOptions.Value.Collection);
    }

    public async Task SaveEvent(Event @event)
    {
        await _events.InsertOneAsync(@event);
    }

    public async Task<IEnumerable<Event>> GetEvents(Guid aggregateId)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.AggregateId, aggregateId);
        return await _events.Find(filter).ToListAsync();
    }

    public Task<int> GetLatestVersionAsync(Guid aggregateId)
    {
        throw new NotImplementedException();
    }
}
