using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpsForge.Application.Interfaces.Repositories;
using OpsForge.Infrastructure.Extensions.DbContext;
using OpsForge.Infrastructure.Persistence.Contexts;
using OpsForge.Infrastructure.Persistence.Repositories;

namespace OpsForge.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddApplicationContexts(cfg, true);

        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();

        return services;
    }
}
