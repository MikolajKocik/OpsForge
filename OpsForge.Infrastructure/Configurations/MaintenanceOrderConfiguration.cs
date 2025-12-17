using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsForge.Domain.Entities.AggregateMaintenance;

namespace OpsForge.Infrastructure.Configurations;

internal sealed class MaintenanceOrderConfiguration : IEntityTypeConfiguration<MaintenanceOrder>
{
    public void Configure(EntityTypeBuilder<MaintenanceOrder> builder)
    {
        builder.HasKey(mo => mo.Id);

        // todo relationship
        
        builder.Navigation(mo => mo.Statuses)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
