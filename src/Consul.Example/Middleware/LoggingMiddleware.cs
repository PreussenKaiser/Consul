using Consul.Abstractions;
using Microsoft.Extensions.Logging;

namespace Consul.Example.Middleware;

public sealed class LoggingMiddleware : IMiddleware
{
    private readonly ILogger<LoggingMiddleware> logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        this.logger = logger;
    }

    public Task InvokeAsync()
    {
        this.logger.LogInformation("Executing command...");

        return Task.CompletedTask;
    }
}
