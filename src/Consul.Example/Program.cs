using Consul.Bootstrapping;
using Consul.Example.Middleware;

var builder = ConsoleApplication.CreateDefaultBuilder(args);

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

await app.RunAsync();