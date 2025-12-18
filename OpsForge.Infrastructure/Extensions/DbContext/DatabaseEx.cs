using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpsForge.Infrastructure.Persistence.Contexts;

namespace OpsForge.Infrastructure.Extensions.DbContext;

public static class DatabaseEx
{
    /// <summary>
    /// Registers the application's database contexts and configures Entity Framework Core services for use with SQL
    /// Server and second-level caching.
    /// </summary>
    /// <remarks>This method adds <see cref="MachineContext"/> and <see cref="MaintenanceContext"/> to the
    /// service collection, configured to use SQL Server with retry-on-failure enabled. It also configures Entity
    /// Framework Core second-level caching using an in-memory cache provider.</remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the database contexts and related services are added.</param>
    /// <param name="cfg">The <see cref="IConfiguration"/> instance used to retrieve connection strings and configuration settings.</param>
    /// <param name="isDevelopment"><see langword="true"/> to enable development-specific Entity Framework Core options such as sensitive data
    /// logging and detailed errors; otherwise, <see langword="false"/>.</param>
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
