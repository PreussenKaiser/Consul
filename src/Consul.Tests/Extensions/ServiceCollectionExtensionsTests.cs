using Consul.Abstractions;
using Consul.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Consul.Tests.Extensions;

public sealed class ServiceCollectionExtensionsTests
{
    private readonly IServiceCollection services = new ServiceCollection();

    [Fact]
    public void Adds_Middleware()
    {
        // Arrange
        Assembly? assembly = Assembly.GetExecutingAssembly();
        IServiceProvider serviceProvider = this.services
            .AddFromAssembly<IMiddleware>(assembly)
            .BuildServiceProvider();

        // Act
        IEnumerable<IMiddleware> actual = serviceProvider.GetServices<IMiddleware>();

        // Assert
        Assert.NotEmpty(actual);
    }
}

public sealed class MockMiddleware : IMiddleware
{
    public Task InvokeAsync() => Task.CompletedTask;
}
