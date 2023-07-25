using Microsoft.Extensions.Logging;

namespace Consul.Example.Commands;

public sealed class HelloCommand : CommandBase
{
    private readonly ILogger<HelloCommand> logger;

    public HelloCommand(ILogger<HelloCommand> logger)
    {
        this.logger = logger;

        this.Name = "hello";
    }

    public override Task RunAsync()
    {
        this.logger.LogInformation("Hello, world!");

        return Task.CompletedTask;
    }
}
