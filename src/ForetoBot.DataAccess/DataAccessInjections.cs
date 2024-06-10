using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;

namespace ForetoBot.DataAccess;

public static class DataAccessInjections
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddMarten(options =>
            {
                options.Connection(configuration.GetConnectionString("default")!);
                options.AutoCreateSchemaObjects = AutoCreate.CreateOnly;
            })
            .ApplyAllDatabaseChangesOnStartup()
            .OptimizeArtifactWorkflow().Services
            .ConfigureStore();
    }

    private static IServiceCollection ConfigureStore(this IServiceCollection services)
    {
        return services
            .ConfigureMarten(options =>
            {
                // options.RegisterDocumentType<ModelName>();
            });
    }
}