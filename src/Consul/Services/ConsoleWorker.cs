using Consul.Abstractions;
using Consul.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Consul.Services;

public sealed class ConsoleWorker : IConsoleWorker
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<ConsoleWorker> logger;
    private readonly string[] arguments;

    public ConsoleWorker(
        IServiceProvider serviceProvider,
        ILogger<ConsoleWorker> logger,
        ConsoleArguments arguments)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
        this.arguments = arguments.Value;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        using (IServiceScope scope = this.serviceProvider.CreateScope())
        {
            var command = this.serviceProvider
                .GetServices<CommandBase>()
                .FirstOrDefault(c => c.CommandName == this.arguments[0]);

            if (command is not null)
            {
                await command!.ExecuteAsync(this.arguments);
            }
        }
    }
}
