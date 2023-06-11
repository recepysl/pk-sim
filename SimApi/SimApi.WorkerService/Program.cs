using SimApi.WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<LogWorker>();
    })
    .Build();

await host.RunAsync();
