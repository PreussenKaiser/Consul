using Consul.Bootstrapping;
using Consul.Example.Middleware;
using Consul.Extensions;

var builder = ConsoleApplication
    .CreateBuilder(args)
    .ConfigureServices(services => services
        .AddCommandsFromAssembly<Program>()
        .AddConsoleLogging());

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

await app.RunAsync();