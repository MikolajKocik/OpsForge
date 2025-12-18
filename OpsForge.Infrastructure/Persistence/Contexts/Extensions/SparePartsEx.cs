using Microsoft.EntityFrameworkCore;
using OpsForge.Domain.Entities.AggregateMachine.Machines;

namespace OpsForge.Infrastructure.Persistence.Contexts.Extensions;

internal static class SparePartsEx
{
    /// <summary>
    /// Configures all <see cref="SparePart"/> properties of the <see cref="AutomaticMeldingMachine"/> entity as owned
    /// types and adds a corresponding shadow property to track the last replacement date for each spare part.
    /// </summary>
    /// <remarks>For each property of type <see cref="SparePart"/> on the <see
    /// cref="AutomaticMeldingMachine"/> entity, this method configures the property as an owned entity and adds a
    /// shadow property named <c>{PropertyName}_LastReplaced</c> of type <see cref="DateTime"/>. The shadow property can
    /// be used to store the date when the corresponding spare part was last replaced.</remarks>
    /// <param name="builder">The <see cref="ModelBuilder"/> instance used to configure the entity model.</param>
    /// <returns>The same <see cref="ModelBuilder"/> instance so that additional configuration calls can be chained.</returns>
    public static ModelBuilder AutomateSparePartsShadowProperty(this ModelBuilder builder)
    {
        var machineEntity = builder.Entity<AutomaticMeldingMachine>();

        // get all properties who are the SparePart record
        var sparePartsProperties = typeof(AutomaticMeldingMachine)
            .GetProperties()
            .Where(p => p.PropertyType == typeof(SparePart));

        foreach (var prop in sparePartsProperties)
        {
            // set spare part record as owned type
            machineEntity.OwnsOne(prop.PropertyType, prop.Name);

            // add shadow property for last replaced spare part date
            machineEntity.Property<DateTime>($"{prop.Name}_LastReplaced");
        }

        return builder;
    }
}
