using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // services.AddAutoMapper(Assembly.GetExecutingAssembly()); Maybe?
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); TODO
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}