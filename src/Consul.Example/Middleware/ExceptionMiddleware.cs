using Consul.Middleware;
using Microsoft.Extensions.Logging;

namespace Consul.Example.Middleware;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly CommandDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(
        CommandDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public Task InvokeAsync()
    {
        try
        {
            // TODO: Execute action here.
        }
        catch (Exception ex)
        {
            this.logger.LogError("An error occurred:\n{ex}", ex);
        }

        return Task.CompletedTask;
    }
}
