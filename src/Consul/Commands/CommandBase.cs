using Consul.Extensions;

namespace Consul.Commands;

public abstract class CommandBase
{
    private readonly IDictionary<string, string> parameters;
    private readonly IList<Action<string>> mappings;

    public CommandBase()
    {
        this.CommandName = string.Empty;
        this.parameters = new Dictionary<string, string>();
        this.mappings = new List<Action<string>>();
    }

    public string CommandName { get; private set; }

    protected abstract Task RunAsync();

    public async Task ExecuteAsync(params string[] arguments)
    {
        string[] parsedArguments = arguments.ParseArguments();

        for (var i = 0; i < mappings.Count; i++)
        {
            Action<string> mapping = this.mappings[i];
            string argument = parsedArguments[i];

            mapping.Invoke(argument);
        }

        await this.RunAsync();
    }

    protected void IsCommand(string commandName)
    {
        this.CommandName = commandName.ToLower();
    }

    protected void AddParameter(string name, string description, Action<string> mapping)
    {
        this.parameters.Add(name.ToLower(), description);
        this.mappings.Add(mapping);
    }
}
