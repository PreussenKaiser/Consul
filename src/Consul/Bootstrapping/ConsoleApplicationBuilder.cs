using Microsoft.Extensions.DependencyInjection;

namespace Consul.Bootstrapping;

public sealed class ConsoleApplicationBuilder
{
    private readonly string[] arguments;

    public ConsoleApplicationBuilder(params string[] arguments)
    {
        this.arguments = arguments;
        this.Services = new ServiceCollection();
    }

    public IServiceCollection Services { get; }

    public ConsoleApplicationBuilder ConfigureServices(Func<IServiceCollection, IServiceCollection> action)
    {
        action(this.Services);

        return this;
    }

    public ConsoleApplication Build()
    {
        return new ConsoleApplication(this.Services.BuildServiceProvider(), this.arguments);
    }
}
