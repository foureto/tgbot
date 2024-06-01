using Flour.YandexSpeechKit;
using ForetoBot.Business.Jobs;
using ForetoBot.Business.Services.Telegram;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForetoBot.Business;

public static class BusinessInjections
{
    public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddTelegram(configuration)
            .AddSpeechKit(configuration)
            .AddHostedService<InitialJob>();
    }
}