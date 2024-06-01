using Microsoft.Extensions.Hosting;

namespace ForetoBot.Business.Jobs;

internal class InitialJob : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
    }
}