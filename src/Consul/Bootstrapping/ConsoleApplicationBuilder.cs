using Microsoft.Extensions.DependencyInjection;

namespace Consul.Bootstrapping;

public sealed class ConsoleApplicationBuilder
{
    public ConsoleApplicationBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }

    public ConsoleApplicationBuilder ConfigureServices(Func<IServiceCollection, IServiceCollection> action)
    {
        action(Services);

        return this;
    }

    public ConsoleApplication Build()
    {
        return new ConsoleApplication(Services.BuildServiceProvider());
    }
}
