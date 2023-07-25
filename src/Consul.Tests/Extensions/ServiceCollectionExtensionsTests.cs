using Consul.Commands;
using Consul.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Consul.Tests.Extensions;

public sealed class ServiceCollectionExtensionsTests
{
    [Theory]
    [InlineData(typeof(MockCommand), true)]
    [InlineData(typeof(string), false)]
    public void Gets_Commands(Type type, bool expected)
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        services.AddCommandsFromAssembly(type);

        bool actual = services
            .BuildServiceProvider()
            .GetService<CommandBase>() is not null;

        // Assert
        Assert.Equal(expected, actual);
    }
}

public sealed class MockCommand : CommandBase
{
    public override Task RunAsync()
    {
        return Task.CompletedTask;
    }
}
