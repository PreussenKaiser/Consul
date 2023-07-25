using Consul.Commands;
using Consul.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Consul.Bootstrapping;

public sealed class ConsoleApplication
{
    public ConsoleApplication(
        IServiceProvider serviceProvider,
        params string[] arguments)
    {
        this.ServiceProvider = serviceProvider;
        this.Arguments = arguments;
    }

    public IServiceProvider ServiceProvider { get; }

    public string[] Arguments { get; }

    public static ConsoleApplicationBuilder CreateBuilder(params string[] args)
    {
        return new ConsoleApplicationBuilder(args);
    }

    public ConsoleApplication UseMiddleware<TMiddleware>()
        where TMiddleware : notnull, IMiddleware
    {
        // TODO: Add middleware.

        return this;
    }

    public async Task RunAsync()
    {
        using (IServiceScope scope = this.ServiceProvider.CreateScope())
        {
            ILogger<ConsoleApplication> logger = this.ServiceProvider.GetRequiredService<ILogger<ConsoleApplication>>();
            IEnumerable<CommandBase> commands = this.ServiceProvider.GetServices<CommandBase>();

            foreach (CommandBase command in commands)
            {
                if (command.Name == this.Arguments[0])
                {
                    await command.RunAsync();
                }
            }
        }

        this.PromptUser(this.ServiceProvider);
    }

    private void PromptUser(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<ConsoleApplication>>();

#if DEBUG
        logger.LogInformation("Press any key to continue.");

        Console.ReadKey();
#endif
    }
}
