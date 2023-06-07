using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence;
using Domain.Interfaces;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection("MongoDbSettings"));
        
        services.AddScoped<IEventStore, MongoDbEventStore>();
        services.AddScoped(typeof(IEventSourcedRepository<>), typeof(EventSourcedRepository<>));

        return services;
    }
}
