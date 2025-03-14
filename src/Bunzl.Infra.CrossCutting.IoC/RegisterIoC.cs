using System.Reflection;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bunzl.Infra.CrossCutting.IoC;

public static class RegisterIoC
{
    public static IServiceCollection AddInject(this IServiceCollection services, Assembly[] assemblies) =>
        services
            .AddInject(assemblies, typeof(IInjectSingleton), ServiceLifetime.Singleton)
            .AddInject(assemblies, typeof(IInjectTransient), ServiceLifetime.Transient)
            .AddInject(assemblies, typeof(IInjectScoped), ServiceLifetime.Scoped);

    private static IServiceCollection AddInject(this IServiceCollection services, Assembly[] assemblies,
        Type interfaceType, ServiceLifetime lifetime)
    {
        var interfaceTypes = assemblies.SelectMany(s => s.GetTypes())
            .Where(t => interfaceType.IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

        foreach (var serviceType in interfaceTypes)
        {
            // Get all interface that the class implemented
            var implementedInterfaces = serviceType.GetInterfaces()
                .Where(i => i.Name != interfaceType.Name);

            // Class implemented interface
            var interfaces = implementedInterfaces.ToList();
            
            if (interfaces.Count != 0)
            {
                foreach (var @interface in interfaces)
                {
                    AddServiceInject(services, @interface, serviceType, lifetime);
                }
            }
            else
            {
                // Class doesn't implemented interface
                AddServiceInject(services, null, serviceType, lifetime);
            }
        }

        return services;
    }

    private static IServiceCollection AddServiceInject(IServiceCollection services, Type? @interface, Type serviceType,
        ServiceLifetime lifetime)
    {
        return lifetime switch
        {
            ServiceLifetime.Singleton => @interface != null
                ? services.AddSingleton(@interface, serviceType)
                : services.AddSingleton(serviceType),
            ServiceLifetime.Scoped => @interface != null
                ? services.AddScoped(@interface, serviceType)
                : services.AddScoped(serviceType),
            ServiceLifetime.Transient => @interface != null
                ? services.AddTransient(@interface, serviceType)
                : services.AddTransient(serviceType),
            _ => services,
        };
    }
}