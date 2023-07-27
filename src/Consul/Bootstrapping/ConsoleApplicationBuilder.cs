using Microsoft.Extensions.DependencyInjection;

namespace Consul.Bootstrapping;

public sealed class ConsoleApplicationBuilder
{
    public ConsoleApplicationBuilder(IServiceCollection services)
    {
        this.Services = services;
    }

    public IServiceCollection Services { get; }

    public ConsoleApplicationBuilder ConfigureServices(Func<IServiceCollection, IServiceCollection> action)
    {
        action(this.Services);

        return this;
    }

    public ConsoleApplication Build()
    {
        return new ConsoleApplication(this.Services.BuildServiceProvider());
    }
}
