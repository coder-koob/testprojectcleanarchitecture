using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence;
using Domain.Interfaces;
using StackExchange.Redis;
using Infrastructure.ReadModels;
using Application.Offices.ReadModels;
using Application.Doors.ReadModels;
using Domain.Common;
using MongoDB.Bson.Serialization;
using Application.Common.Services;
using Infrastructure.Identity;
using Application.Common.Security;
using Microsoft.IdentityModel.Logging;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(Event).Assembly;

        foreach (var type in assembly.GetTypes())
        {
            if (type.IsSubclassOf(typeof(Event)))
            {
                var method = typeof(BsonClassMap)
                    .GetMethod("RegisterClassMap", new[] { typeof(Action<BsonClassMap>) })
                    ?.MakeGenericMethod(type);
                method?.Invoke(null, new object[] { new Action<BsonClassMap>(cm => cm.AutoMap()) });
            }
        }

        services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.MongoDbSettings));
        
        var redisOptions = new RedisDbOptions();
        configuration.GetSection(RedisDbOptions.RedisDbSettings).Bind(redisOptions);

        var redisConfigurationOptions = ConfigurationOptions.Parse(redisOptions.ConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfigurationOptions));

        services.AddTransient<IReadModelService<OfficeReadModel>, RedisReadModelService<OfficeReadModel>>();
        services.AddTransient<IReadModelService<DoorHistoryReadModel>, RedisReadModelService<DoorHistoryReadModel>>();

        services.AddScoped<IEventStore, MongoDbEventStore>();
        services.AddScoped(typeof(IEventSourcedRepository<>), typeof(EventSourcedRepository<>));

        services.AddIdentityServer()
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients);

        services.AddTransient<IClientService, ClientService>();

        #if DEBUG
            IdentityModelEventSource.ShowPII = true;
        #endif

        services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:7055";
                options.TokenValidationParameters.ValidateAudience = false;

                #if DEBUG
                    options.RequireHttpsMetadata = false;
                #endif
            });

        services.AddAuthorization(options =>
            options.AddPolicy("CreateOffice", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", Config.CreateOfficeScope);
            })
        );

        return services;
    }
}
