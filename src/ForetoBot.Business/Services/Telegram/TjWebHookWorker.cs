using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace ForetoBot.Business.Services.Telegram;

internal class TjWebHookWorker(IServiceProvider serviceProvider, ILogger<TjWebHookWorker> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Setting up webhook address");
        await using var scope = serviceProvider.CreateAsyncScope();
        var settings = scope.ServiceProvider.GetRequiredService<IOptions<TelegramSettings>>();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
        
        var webhookAddress = $"{settings.Value.WebHookAddress.TrimEnd('/')}/bot/hook";
        
        await botClient.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: Array.Empty<UpdateType>(),
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing webhook address");
        await using var scope = serviceProvider.CreateAsyncScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
        await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}