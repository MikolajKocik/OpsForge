using Microsoft.EntityFrameworkCore;
using OpsForge.Domain.Entities.AggregateMaintenance;
using OpsForge.Domain.SeedWork.Interfaces;
using OpsForge.Infrastructure.Persistence.Annotations;
using OpsForge.Infrastructure.Utilities;

namespace OpsForge.Infrastructure.Persistence.Contexts;

internal sealed class MaintenanceContext : DbContext, IUnitOfWork
{
    internal DbSet<MaintenanceOrder> Maintenances { get; set; }

    internal MaintenanceContext(DbContextOptions<MaintenanceContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasAnnotation(ProductionAnnotations.OwnerTeam, "Maintenance & Logistics (MLX)");
        modelBuilder.HasAnnotation(ProductionAnnotations.SchemaPrefix, "maint_logs");
        modelBuilder.HasAnnotation(ProductionAnnotations.ContextCriticality, "Tier 2 - Operational Support");
        modelBuilder.HasAnnotation(ProductionAnnotations.Description,
            "The Maintenance Context manages all work orders, failure reports," +
            " scheduled service logs, and historical maintenance actions related to production assets.");

        modelBuilder.HasDefaultSchema(ContextUtility.MaintenanceSchema);

        modelBuilder.Entity<MaintenanceOrder>(entity =>
        {
            entity.Property(e => e.Id)
               .UseHiLo(Hilo.Name)
               .HasField("_id");

            entity.ToTable<MaintenanceOrder>(ContextUtility.MaintenanceTable);
        });

        //modelBuilder.ApplyConfigurationsFromAssembly();
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
