using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpsForge.Infrastructure.Persistence.Contexts;

namespace OpsForge.Infrastructure.Extensions.DbContext;

public static class DatabaseEx
{
    public static void AddApplicationContexts(this IServiceCollection services, IConfiguration cfg, bool isDevelopment)
    {
        Action<DbContextOptionsBuilder> cfgOptions = options =>
        {
            options.UseSqlServer(cfg.GetConnectionString("SqlServer"), sql =>
            {
                sql.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            });

            if (isDevelopment)
            {
                options.EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
        };

        // contexts config
        services.AddDbContext<MachineContext>(cfgOptions);
        services.AddDbContext<MaintenanceContext>(cfgOptions);

        // cache config
        services.AddEFSecondLevelCache(options =>
                  options.UseMemoryCacheProvider()
                      .ConfigureLogging(true)
                      .UseCacheKeyPrefix("EF_")
                      .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromMinutes(1)));
    }
}
