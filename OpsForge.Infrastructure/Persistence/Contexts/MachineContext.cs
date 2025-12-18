using Microsoft.EntityFrameworkCore;
using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.SeedWork.Interfaces;
using OpsForge.Infrastructure.Persistence.Annotations;
using OpsForge.Infrastructure.Persistence.Contexts.Extensions;
using OpsForge.Infrastructure.Utilities;
using System.Reflection;

namespace OpsForge.Infrastructure.Persistence.Contexts;

public sealed class MachineContext : DbContext, IUnitOfWork
{
    internal DbSet<Machine> Machines { get; set; }

    public MachineContext(DbContextOptions<MachineContext> options)
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

        modelBuilder.AutomateSparePartsShadowProperty();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete. The default value is <see
    /// cref="CancellationToken.None"/>.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
    /// written to the database.</returns>
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
