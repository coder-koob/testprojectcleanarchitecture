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
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Application.Common.Services;
using Microsoft.AspNetCore.Authentication;
using Infrastructure.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;
using Application.Common.Security;

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

        services.Configure<MongoDbOptions>(configuration.GetSection("MongoDbSettings"));

        var configurationOptions = ConfigurationOptions.Parse("127.0.0.1:6380");
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configurationOptions));

        services.AddTransient<IReadModelService<OfficeReadModel>, RedisReadModelService<OfficeReadModel>>();
        services.AddTransient<IReadModelService<DoorHistoryReadModel>, RedisReadModelService<DoorHistoryReadModel>>();
        
        services.AddScoped<IEventStore, MongoDbEventStore>();
        services.AddScoped(typeof(IEventSourcedRepository<>), typeof(EventSourcedRepository<>));

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
