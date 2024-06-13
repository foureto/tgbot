using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForetoBot.Business.Services.FileStore;

internal static class GoogleInjections
{
    public static IServiceCollection AddGoogle(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<GoogleStoreSettings>(e => configuration.GetSection("storage:google").Bind(e))
            .AddSingleton<IFileStore, GoogleFileStore>();
    }
}