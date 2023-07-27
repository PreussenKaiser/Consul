namespace Consul.Services;

public interface IConsoleWorker
{
    Task RunAsync(CancellationToken cancellationToken);
}
