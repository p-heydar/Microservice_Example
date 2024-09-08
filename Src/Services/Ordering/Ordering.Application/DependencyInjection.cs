using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        // MediatR  
        var executingAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(configuration: config =>
        {
            config.RegisterServicesFromAssembly(executingAssembly);
        });

        return services;
    }
}