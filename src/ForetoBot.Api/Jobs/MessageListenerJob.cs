using ForetoBot.Api.Commons.Settings;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ForetoBot.Api.Jobs;

internal class MessageListenerJob(
    IOptions<TelegramSettings> options,
    ILogger<MessageListenerJob> logger) : BackgroundService
{
    private readonly TelegramSettings _settings = options.Value;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_settings.UseWebhooks)
            return base.StopAsync(stoppingToken);

        var botClient = new TelegramBotClient(_settings.ApiKey);
        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() },
            cancellationToken: stoppingToken
        );

        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message is not null)
            await client.SendTextMessageAsync(update.Message.Chat.Id, $"Halo!", cancellationToken: token);
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        logger.LogError(exception, "Something bad");
        return Task.CompletedTask;
    }
}