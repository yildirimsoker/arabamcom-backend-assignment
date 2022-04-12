using Arabam.Com.Application.DI;
using Arabam.Com.Infrastructure.DI;
using Arabam.Com.WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddApplication();
        services.AddInfrastructure();
        services.AddHostedService<Worker>();
        
    })
    .Build();

await host.RunAsync();
