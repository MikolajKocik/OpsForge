using Microsoft.EntityFrameworkCore;
using OpsForge.Domain.Entities;
using OpsForge.Domain.SeedWork.Interfaces;
using OpsForge.Infrastructure.Persistence.Annotations;
using OpsForge.Infrastructure.Utilities;
using System.Reflection;

namespace OpsForge.Infrastructure.Persistence.Contexts;

internal sealed class MachineContext : DbContext, IUnitOfWork
{
    internal DbSet<Machine> Machines { get; set; }

    internal MachineContext(DbContextOptions<MachineContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasAnnotation(ProductionAnnotations.OwnerTeam, "Manufacturing Excellence (MEx)");
        modelBuilder.HasAnnotation(ProductionAnnotations.SchemaPrefix, "prod_mach");
        modelBuilder.HasAnnotation(ProductionAnnotations.ContextCriticality, "Tier 1 - Production Critical");
        modelBuilder.HasAnnotation(ProductionAnnotations.Description,
            "The Machine Context manages all static and dynamic data related to production floor machines," +
            " including configuration and operational metrics.");

        modelBuilder.HasDefaultSchema(ContextUtility.MachineSchema);

        modelBuilder.Entity<Machine>().ToTable(ContextUtility.MachineTable);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
