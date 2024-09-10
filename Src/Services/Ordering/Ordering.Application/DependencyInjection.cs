﻿using System.Reflection;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        // MediatR  
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            //config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        return services;
    }
}