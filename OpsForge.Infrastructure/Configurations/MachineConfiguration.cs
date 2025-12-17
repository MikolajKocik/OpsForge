using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.Enums;
using OpsForge.Domain.SeedWork;

namespace OpsForge.Infrastructure.Configurations
{
    // base configurations for machine aggregate
    internal sealed class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasAlternateKey(m => m.Code);

            builder.Property(m => m.Name)
            .IsRequired().HasMaxLength(50);

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

            // value objects
            builder.OwnsOne(m => m.Inventory);
            builder.OwnsOne(m => m.Specification);

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
}
