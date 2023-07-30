using Consul.Abstractions;

namespace Consul.Services;

public sealed class ConsoleApplicationLifetime : IConsoleApplicationLifetime
{
    private readonly CancellationTokenSource tokenSource;

    public ConsoleApplicationLifetime()
    {
        this.tokenSource = new CancellationTokenSource();
    }

    public CancellationToken Token => this.tokenSource.Token;

    public void StopApplication()
    {
        this.tokenSource.Cancel();
    }
}
