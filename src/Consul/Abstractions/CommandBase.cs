using Consul.Extensions;

namespace Consul.Abstractions;

public abstract class CommandBase
{
    private readonly IDictionary<string, string> parameters;
    private readonly IList<Action<string>> mappings;

    public CommandBase()
    {
        CommandName = string.Empty;
        parameters = new Dictionary<string, string>();
        mappings = new List<Action<string>>();
    }

    public string CommandName { get; private set; }

    protected abstract Task RunAsync();

    public async Task ExecuteAsync(params string[] arguments)
    {
        string[] parsedArguments = arguments.ParseArguments();

        for (var i = 0; i < mappings.Count; i++)
        {
            Action<string> mapping = mappings[i];
            string argument = parsedArguments[i];

            mapping.Invoke(argument);
        }

        await RunAsync();
    }

    protected void IsCommand(string commandName)
    {
        CommandName = commandName.ToLower();
    }

    protected void AddParameter(string name, string description, Action<string> mapping)
    {
        parameters.Add(name.ToLower(), description);
        mappings.Add(mapping);
    }
}
