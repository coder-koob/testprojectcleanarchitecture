using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Xunit;

namespace Infrastructure.IntegrationTests.Persistence;

public class MongoDbEventStoreTests
{
    [Fact]
    public async Task SaveEvent_Should_Save_Event_To_Event_Store()
    {
        // Arrange
        var options = Options.Create(new MongoDbOptions 
        {
            ConnectionString = "mongodb://localhost:27017",
            Database = "eventstoretests",
            Collection = "events"
        });

        var eventStore = new MongoDbEventStore(options);
        var aggregateId = Guid.NewGuid();

        // Act
        await eventStore.SaveEvent(new FakeEvent(aggregateId));

        // Assert
        var result = await eventStore.GetEvents(aggregateId);

        Assert.NotNull(result);
        Assert.Equal(aggregateId, result.First().AggregateId);
    }
}

public class FakeEvent : Event
{
    public FakeEvent(Guid aggregateId)
        : base(aggregateId, new Context())
    {
    }
}