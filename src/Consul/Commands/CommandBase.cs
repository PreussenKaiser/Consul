namespace Consul.Commands;

public abstract class CommandBase
{
    public CommandBase()
    {
        Name = string.Empty;
        Arguments = Array.Empty<string>();
    }

    public string Name { get; set; }

    public string[] Arguments { get; }

    public abstract Task RunAsync();
}
