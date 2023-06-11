namespace SimApi.WorkerService;

public class LogWorker : BackgroundService
{
	private readonly ILogger<LogWorker> logger;
	private Task executingTask;
	private CancellationTokenSource cts;

	public LogWorker(ILogger<LogWorker> logger)
	{
		this.logger = logger;
	}

	public override Task StartAsync(CancellationToken cancellationToken)
	{
		logger.LogWarning("Worker service started.");

		cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		executingTask = ExecuteAsync(cts.Token);

		return executingTask.IsCompleted ? executingTask : Task.CompletedTask;
	}

	public override Task StopAsync(CancellationToken cancellationToken)
	{
		if (executingTask == null)
		{
			return Task.CompletedTask;
		}

		logger.LogWarning("Worker service stopping.");

		cts.Cancel();

		Task.WhenAny(executingTask, Task.Delay(-1, cancellationToken)).ConfigureAwait(true);

		cancellationToken.ThrowIfCancellationRequested();

		logger.LogWarning("Worker service stopped.");

		return Task.CompletedTask;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			await Task.Delay(1000, stoppingToken);
		}
	}
}