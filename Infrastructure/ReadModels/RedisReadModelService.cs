using System.Text.Json;
using Domain.Common;
using Domain.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.ReadModels;

public class RedisReadModelService<TReadModel> : IReadModelService<TReadModel> where TReadModel : ReadModel, new()
{
    private readonly IDatabase _db;

    public RedisReadModelService(IConnectionMultiplexer connectionMultiplexer)
    {
        _db = connectionMultiplexer.GetDatabase();
    }

    public async Task<TReadModel?> GetByIdAsync(string id)
    {
        var readModel = new TReadModel();

        var value = await _db.StringGetAsync(readModel.GenerateHashSetName(id));

        return value.IsNullOrEmpty ? null : JsonSerializer.Deserialize<TReadModel>(value!);
    }

    public async Task<TReadModel> SaveAsync(TReadModel readModel, string? id = null)
    {
        var serializedValue = JsonSerializer.Serialize(readModel);

        await _db.StringSetAsync(readModel.GenerateHashSetName(id), serializedValue);

        return readModel;
    }
}