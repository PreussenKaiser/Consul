namespace Consul.Middleware;

public interface IMiddleware
{
    Task InvokeAsync();
}
