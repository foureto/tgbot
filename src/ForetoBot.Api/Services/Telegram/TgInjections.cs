using ForetoBot.Api.Commons.Settings;
using ForetoBot.Api.Jobs;

namespace ForetoBot.Api.Services.Telegram;

internal static class TgInjections
{
    public static IServiceCollection AddTelegram(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<TelegramSettings>(e => configuration.GetSection("telegram").Bind(e))
            .AddHostedService<MessageListenerJob>();
    }
}