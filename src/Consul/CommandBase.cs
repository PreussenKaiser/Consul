namespace Consul;

public abstract class CommandBase
{
    public CommandBase()
    {
        this.Name = string.Empty;
        this.Arguments = Array.Empty<string>();
    }

    public string Name { get; set; }

    public string[] Arguments { get; }

    public abstract Task RunAsync();
}
