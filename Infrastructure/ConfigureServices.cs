using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence;
using Domain.Interfaces;
using StackExchange.Redis;
using Infrastructure.ReadModels;
using Application.Offices.ReadModels;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection("MongoDbSettings"));

        var configurationOptions = ConfigurationOptions.Parse("127.0.0.1:6380");
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configurationOptions));

        services.AddTransient<IReadModelService<OfficeReadModel>, RedisReadModelService<OfficeReadModel>>();
        
        services.AddScoped<IEventStore, MongoDbEventStore>();
        services.AddScoped(typeof(IEventSourcedRepository<>), typeof(EventSourcedRepository<>));

        return services;
    }
}
