using Microsoft.EntityFrameworkCore;
using OpsForge.Domain.SeedWork.Interfaces;

namespace OpsForge.Infrastructure.Utilities;

internal static class ContextUtility
{
    public const string MachineSchema = "Machines";
    public const string MachineTable = "Machine";

    public const string MaintenanceSchema = "Maintenances";
    public const string MaintenanceTable = "Maintenance";

    public static IQueryable<T> ApplySpecification<T>(IQueryable<T> query, ISpecification<T> spec)
        where T : class
    {
        IQueryable<T> queyrableWithIncludes = spec.Includes
            .Aggregate(query,
                (current, include) => current.Include(include));

        IQueryable<T> secondaryResult = spec.IncludeStrings
            .Aggregate(queyrableWithIncludes,
                (current, include) => current.Include(include));

        return secondaryResult.Where(spec.Criteria);
    }
}
