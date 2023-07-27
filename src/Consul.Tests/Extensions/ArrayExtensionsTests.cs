using Consul.Extensions;

namespace Consul.Tests.Extensions;

public sealed class ArrayExtensionsTests
{
    [Fact]
    public void Parses_Arguments()
    {
        // Arrange
        var arguments = new string[3] { "hello", "-name", "world" };
        var expected = new string[1] { "world" };

        // Act
        string[] actual = arguments.ParseArguments();

        // Assert
        Assert.Equal(expected, actual);
    }
}
