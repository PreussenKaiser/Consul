using Microsoft.Extensions.DependencyInjection;

namespace Consul.Bootstrapping;

public sealed class ConsoleApplication
{
    public ConsoleApplication(
        IServiceProvider serviceProvider,
        params string[] arguments)
    {
        this.ServiceProvider = serviceProvider;
        this.Arguments = arguments;
    }

    public static ConsoleApplicationBuilder CreateBuilder(params string[] args)
    {
        return new ConsoleApplicationBuilder(args);
    }

    public IServiceProvider ServiceProvider { get; }

    public string[] Arguments { get; }

    public async Task RunAsync()
    {
        using IServiceScope scope = this.ServiceProvider.CreateScope();

        IEnumerable<CommandBase> commands = this.ServiceProvider.GetServices<CommandBase>();

        foreach (CommandBase command in commands)
        {
            if (command.Name == this.Arguments[0])
            {
                await command.RunAsync();
            }
        }
    }
}
