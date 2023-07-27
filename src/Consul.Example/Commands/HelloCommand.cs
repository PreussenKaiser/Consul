using Consul.Commands;
using Microsoft.Extensions.Logging;

namespace Consul.Example.Commands;

public sealed class HelloCommand : CommandBase
{
    private readonly ILogger<HelloCommand> logger;
    private string name;

    public HelloCommand(ILogger<HelloCommand> logger)
    {
        this.logger = logger;
        this.name = string.Empty;

        base.IsCommand("Hello");
        base.AddParameter("name", "Who to say hello to!", n => this.name = n);
    }

    protected override Task RunAsync()
    {
        this.logger.LogInformation("Hello, {name}!", this.name);

        return Task.CompletedTask;
    }
}
