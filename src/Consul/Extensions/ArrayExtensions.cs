using System.Text;

namespace Consul.Extensions;

public static class ArrayExtensions
{
    public static string[] ParseArguments(this string[] arguments)
    {
        string[] argumentsWithoutCommand = arguments.Skip(1).ToArray();
        ICollection<string> parsedArguments = new List<string>();

        for (var i = 1; i <= argumentsWithoutCommand.Length; i += 2)
        {
            string argument = argumentsWithoutCommand[i];

            parsedArguments.Add(argument);
        }

        return parsedArguments.ToArray();
    }
}
