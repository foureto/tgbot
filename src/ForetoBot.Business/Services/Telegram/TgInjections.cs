using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace ForetoBot.Business.Services.Telegram;

internal static class TgInjections
{
    public static IServiceCollection AddTelegram(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<TelegramSettings>(e => configuration.GetSection("telegram").Bind(e))
            .AddHttpClient("TelegramWebhook")
            .AddTypedClient<ITelegramBotClient>(
                (httpClient, sp) =>
                {
                    var token = sp.GetRequiredService<IOptions<TelegramSettings>>().Value.ApiKey;
                    return new TelegramBotClient(token, httpClient);
                }).Services
            .AddHostedService<TjWebHookWorker>();
    }
}