using Consul.Extensions;

namespace Consul.Tests.Extensions;

public sealed class TypeExtensionsTests
{
    [Theory]
    [InlineData(typeof(MockImplement), true)]
    [InlineData(typeof(MockClass), false)]
    public void Is_Implement(Type type, bool actual)
    {
        // Arrange
        Type interfaceType = typeof(IMockInterface);

        // Act
        bool expected = type.IsImplementOf(interfaceType);

        // Assert
        Assert.Equal(expected, actual);
    }
}

internal interface IMockInterface { }

internal sealed class MockImplement : IMockInterface { }
internal sealed class MockClass { }
