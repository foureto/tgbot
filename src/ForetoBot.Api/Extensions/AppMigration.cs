using ForetoBot.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ForetoBot.Api.Extensions;

internal static class AppMigration
{
    public static async Task<IApplicationBuilder> Migrate(this IApplicationBuilder application)
    {
        await using var scope = application.ApplicationServices.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();

        return application;
    }
}