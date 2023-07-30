using Consul.Abstractions;
using Consul.Entities;
using Consul.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Consul.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandsFromEntryAssembly(this IServiceCollection services)
    {
        IEnumerable<Type>? commandTypes = Assembly
            .GetEntryAssembly()
            ?.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(CommandBase)));

        if (commandTypes is null)
        {
            return services;
        }

        foreach (Type commandType in commandTypes)
        {
            services.AddSingleton(provider => (CommandBase)ActivatorUtilities.CreateInstance(provider, commandType));
        }

        return services;
    }

    public static IServiceCollection AddFromAssembly<T>(
        this IServiceCollection services,
        Assembly? assembly,
        ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where T : class
    {
        IEnumerable<Type>? serviceTypes = assembly?.GetTypes();

        if (serviceTypes is null)
        {
            return services;
        }

        IEnumerable<Type> implementedTypes = serviceTypes.Where(t => t.IsImplementOf<T>());

        foreach (Type implementedType in implementedTypes)
        {
            ServiceDescriptor descriptor = new(typeof(T), implementedType, serviceLifetime);

            services.Add(descriptor);
        }

        return services;
    }

    public static IServiceCollection AddCommandLine(this IServiceCollection services, params string[] arguments)
    {
        return services
            .AddCommandsFromEntryAssembly()
            .AddFromAssembly<IMiddleware>(Assembly.GetEntryAssembly())
            .AddSingleton(new ConsoleArguments(arguments))
            .AddSingleton<IConsoleWorker, ConsoleWorker>()
            .AddSingleton<IConsoleApplicationLifetime, ConsoleApplicationLifetime>();
    }

    public static IServiceCollection AddConsoleLogging(this IServiceCollection services)
    {
        return services.AddLogging(builder => builder.AddConsole());
    }
}
