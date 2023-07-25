using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Consul.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandsFromAssembly<TProgram>(this IServiceCollection services)
        where TProgram : class
    {
        return services.AddCommandsFromAssembly(typeof(TProgram));
    }

    public static IServiceCollection AddCommandsFromAssembly(this IServiceCollection services, Type type)
    {
        IEnumerable<Type>? commandTypes = Assembly
            .GetAssembly(type)
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

    public static IServiceCollection AddConsoleLogging(this IServiceCollection services)
    {
        return services.AddLogging(builder => builder.AddConsole());
    }
}
