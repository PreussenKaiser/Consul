namespace Consul.Abstractions;

public interface IConsoleWorker
{
    Task RunAsync(CancellationToken cancellationToken);
}
