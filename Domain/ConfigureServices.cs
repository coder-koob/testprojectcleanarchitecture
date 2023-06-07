using System.Reflection;
using Domain.Offices;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // services.AddAutoMapper(Assembly.GetExecutingAssembly()); Maybe?
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); TODO
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}