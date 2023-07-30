using Consul.Abstractions;
using Consul.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Consul.Bootstrapping;

public sealed class ConsoleApplication
{
    private readonly List<IMiddleware> middleware;

    public ConsoleApplication(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        middleware = new();
    }

    public IServiceProvider ServiceProvider { get; }

    public static ConsoleApplicationBuilder CreateBuilder()
    {
        IServiceCollection services = new ServiceCollection();

        return new ConsoleApplicationBuilder(services);
    }

    public static ConsoleApplicationBuilder CreateDefaultBuilder(params string[] arguments)
    {
        IServiceCollection services = new ServiceCollection()
            .AddConsoleLogging()
            .AddCommandLine(arguments);

        return new ConsoleApplicationBuilder(services);
    }

    public ConsoleApplication UseMiddleware<TMiddleware>()
        where TMiddleware : notnull, IMiddleware
    {
        IEnumerable<IMiddleware> middleware = ServiceProvider.GetServices<IMiddleware>();
        IMiddleware? foundMiddleware = middleware.FirstOrDefault(m => m.GetType() == typeof(TMiddleware));

        if (foundMiddleware is null)
        {
            return this;
        }

        this.middleware.Add(foundMiddleware);

        return this;
    }

    public async Task RunAsync()
    {
        var worker = ServiceProvider.GetRequiredService<IConsoleWorker>();
        var lifetime = ServiceProvider.GetService<IConsoleApplicationLifetime>();

        CancellationToken token = lifetime?.Token ?? CancellationToken.None;

        middleware.ForEach(async m => await m.InvokeAsync());

        await worker.RunAsync(token);
    }
}
