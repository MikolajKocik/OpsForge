using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine.Machines;
using System.Reflection;

namespace OpsForge.Infrastructure.Persistence.Contexts.Extensions;

internal static class SparePartsEx
{
    /// <summary>
    /// Configures shadow properties for all <see cref="SparePart"/> properties on entity types derived from <see
    /// cref="Machine"/> within the model.
    /// </summary>
    /// <remarks>For each non-abstract entity type that inherits from <see cref="Machine"/>, this method
    /// automatically adds complex shadow properties for each <see cref="SparePart"/> property, including "Brand",
    /// "Model", and "SerialNumber" columns, as well as a nullable <see cref="DateTime"/> shadow property named
    /// "{PropertyName}_LastReplaced". This enables tracking of spare part details and replacement history without
    /// modifying the entity class definitions.</remarks>
    /// <param name="builder">The <see cref="ModelBuilder"/> instance used to configure the entity model.</param>
    /// <returns>The same <see cref="ModelBuilder"/> instance, allowing for fluent configuration chaining.</returns>
    public static ModelBuilder AutomateSparePartsShadowProperty(this ModelBuilder builder)
    {
        // Get all entity types from the model that inherit from Machine and are not abstract
        IEnumerable<IMutableEntityType> machineEntityTypes = builder.Model.GetEntityTypes()
            .Where(e => typeof(Machine).IsAssignableFrom(e.ClrType) && !e.ClrType.IsAbstract);

        foreach (IMutableEntityType entityType in machineEntityTypes)
        {
            // Use reflection to find all properties of type SparePart within the current entity
            IEnumerable<PropertyInfo> properties = entityType.ClrType.GetProperties()
                 .Where(p => p.PropertyType == typeof(SparePart));

            foreach (PropertyInfo prop in properties)
            {
                // Configure each SparePart property as a Complex Type for the database schema
                builder.Entity(entityType.ClrType).ComplexProperty(prop.Name, cpBuilder =>
                {
                    cpBuilder.IsRequired();

                    // Map sub-properties to specific column names
                    cpBuilder.Property("Brand").HasColumnName($"{prop.Name}_Brand").IsRequired();
                    cpBuilder.Property("Model").HasColumnName($"{prop.Name}_Model").IsRequired();
                    cpBuilder.Property("SerialNumber").HasColumnName($"{prop.Name}_SerialNumber");
                });

                // Add a shadow property to the main entity to track the replacement date in the DB
                builder.Entity(entityType.ClrType).Property<DateTime?>($"{prop.Name}_LastReplaced");
            }
        }

        return builder;
    }
}