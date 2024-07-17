using ForetoBot.DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForetoBot.DataAccess;

public static class DataAccessInjections
{
    private const string DefaultSection = "default";

    public static IServiceCollection AddDataAccess(
        this IServiceCollection services, IConfiguration configuration, string sectionName = DefaultSection)
    {
        return services
            .AddDbContextPool<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(sectionName)))
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}