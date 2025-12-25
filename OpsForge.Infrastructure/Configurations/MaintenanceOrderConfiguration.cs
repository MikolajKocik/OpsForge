using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsForge.Domain.Entities.AggregateMaintenance;
using OpsForge.Domain.Enums;
using OpsForge.Domain.SeedWork;

namespace OpsForge.Infrastructure.Configurations;

/// <summary>
/// Provides the Entity Framework Core configuration for the <see cref="MaintenanceOrder"/> entity.
/// </summary>
/// <remarks>This configuration defines the primary key for <see cref="MaintenanceOrder"/> and configures its
/// collection of statuses as an owned entity type mapped to a separate table. It is intended for use with the Entity
/// Framework Core model building infrastructure and is not intended to be used directly in application code.</remarks>
internal sealed class MaintenanceOrderConfiguration : IEntityTypeConfiguration<MaintenanceOrder>
{
    public void Configure(EntityTypeBuilder<MaintenanceOrder> builder)
    {
        builder.HasKey(mo => mo.Id);

        // Configure owned collection of statuses as a separate table
        builder.OwnsMany(mo => mo.Statuses, statuses =>
        {
            statuses.ToTable("MaintenanceStatus");

            // Relationship back to owner with shadow FK
            statuses.WithOwner().HasForeignKey("MaintenanceOrderId");

            // Shadow primary key for the owned rows (avoid clash with Status.Id)
            statuses.Property<int>("MaintenanceStatusId");
            statuses.HasKey("MaintenanceStatusId");

            // Map the Status value (id) to a column
            statuses.Property(s => s.Id)
                .HasColumnName("StatusId");
        });

        // MaintenanceSchedule as owned value object
        builder.OwnsOne(mo => mo.Schedule, schBuilder =>
        {
            schBuilder.Property(mo => mo.Type)
            .HasConversion(
                mot => mot.Id,
                id => Enumeration.GetAll<MaintenanceType>()
                    .First(st => st.Id == id))
            .IsRequired();
            schBuilder.Property(i => i.MaintenanceInterval).IsRequired();
            schBuilder.Property(i => i.LastMaintenanceDate).IsRequired();

            schBuilder.Property(i => i.Notes).HasMaxLength(300);
        });

        builder.Navigation(mo => mo.Statuses)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}