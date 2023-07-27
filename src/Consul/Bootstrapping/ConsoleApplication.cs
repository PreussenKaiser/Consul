using Consul.Entities;
using Consul.Extensions;
using Consul.Middleware;
using Consul.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Consul.Bootstrapping;

public sealed class ConsoleApplication
{
    public ConsoleApplication(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
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
        // TODO: Add middleware.

        return this;
    }

    public async Task RunAsync()
    {
        var worker = this.ServiceProvider.GetRequiredService<IConsoleWorker>();
        CancellationTokenSource tokenSource = new();

        await worker.RunAsync(tokenSource.Token);
    }
}
