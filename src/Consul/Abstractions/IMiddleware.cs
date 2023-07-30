namespace Consul.Abstractions;

public interface IMiddleware
{
    Task InvokeAsync();
}
