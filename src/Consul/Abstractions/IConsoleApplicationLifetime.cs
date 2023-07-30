namespace Consul.Abstractions;

public interface IConsoleApplicationLifetime
{
    CancellationToken Token { get; }

    void StopApplication();
}
