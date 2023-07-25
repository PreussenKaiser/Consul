using Consul.Bootstrapping;
using Consul.Extensions;

await ConsoleApplication
    .CreateBuilder(args)
    .ConfigureServices(services => services
        .AddCommandsFromAssembly<Program>()
        .AddConsoleLogging())
    .Build()
    .RunAsync();