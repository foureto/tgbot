using Flour.YandexSpeechKit;
using ForetoBot.Business.Jobs;
using ForetoBot.Business.Services.FileStore;
using ForetoBot.Business.Services.Telegram;
using ForetoBot.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForetoBot.Business;

public static class BusinessInjections
{
    public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDataAccess(configuration)
            .AddTelegram(configuration)
            .AddSpeechKit(configuration)
            .AddGoogle(configuration)
            .AddHostedService<InitialJob>();
    }
}