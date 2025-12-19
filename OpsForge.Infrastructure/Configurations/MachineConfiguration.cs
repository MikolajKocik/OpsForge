using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.Enums;
using OpsForge.Domain.SeedWork;

namespace OpsForge.Infrastructure.Configurations;

/// <summary>
/// Provides the Entity Framework Core configuration for the <see cref="Machine"/> entity.
/// </summary>
/// <remarks>This configuration defines the entity's keys, property constraints, value object ownership,
/// relationships, and discriminator mapping for derived machine types. It is intended for use with the Entity
/// Framework Core model builder and is not intended to be used directly in application code.</remarks>
internal sealed class MachineConfiguration : IEntityTypeConfiguration<Machine>
{
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
        builder.HasKey(m => m.Id);
        builder.HasAlternateKey(m => m.Code);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(50);

        // config for static enum 'Status'
        builder.Property(m => m.MachineStatus)
            .HasConversion(
                s => s.Id,
                id => Enumeration.GetAll<Status>()
                    .First(st => st.Id == id))
            .IsRequired();

        // config for static enum 'Line'
        builder.Property(m => m.ProductionLine)
            .HasConversion(
                l => l.Id,
                id => Enumeration.GetAll<Line>()
                    .First(li => li.Id == id))
            .IsRequired();

        builder.OwnsOne(m => m.Inventory, inventoryBuilder =>
        {
            inventoryBuilder.ToJson();

            inventoryBuilder.OwnsMany(i => i.Parts, partBuilder =>
            {
                partBuilder.ToJson();
                partBuilder.Property(p => p.SerialNumber);
                partBuilder.Property(p => p.Model).IsRequired();
                partBuilder.Property(p => p.Brand).IsRequired();
            });
        });

        builder.OwnsOne(m => m.Specification, specBuilder =>
        {
            specBuilder.Property("_model").HasColumnName("Model").HasMaxLength(100);
            specBuilder.Property("_manufacturer").HasColumnName("Manufacturer").HasMaxLength(100);
            specBuilder.Property("_powerKw").HasColumnName("PowerKw");
            specBuilder.Property("_voltage").HasColumnName("Voltage");
            specBuilder.Property("_weightKg").HasColumnName("WeightKg");
            specBuilder.Property("_material").HasColumnName("Material");
            specBuilder.Property("_description").HasColumnName("Description").HasMaxLength(100);

            // shadow properties for dimensions
            specBuilder.Property<double>("_dimensions_Length").HasColumnName("Dimension_Length");
            specBuilder.Property<double>("_dimensions_Width").HasColumnName("Dimension_Width");
            specBuilder.Property<double>("_dimensions_Height").HasColumnName("Dimension_Height");
        });


        // relation with maintenances
        builder.HasMany(m => m.Maintenances)
            .WithOne()
            .HasForeignKey("MachineId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(m => m.Maintenances)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // machine types (childs)
        builder.HasDiscriminator<string>("MachineType")
            .HasValue<Machine>("Base")
            .HasValue<AutomaticMeldingMachine>("Automatic")
            .HasValue<CncMillingMachine>("CNC")
            .HasValue<HydraulicPress>("Hydraulic")
            .HasValue<InjectionMeldingMachine>("Injection")
            .HasValue<RoboticAssemblyLine>("Robotic");
    }
}
