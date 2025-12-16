using OpsForge.Domain.SeedWork;
using System.Linq.Expressions;

namespace OpsForge.Domain.Entities.AggregateMaintenance;

public sealed class MaintenanceOrderWithStatusesSpecification : BaseSpecification<MaintenanceOrder>
{
    public MaintenanceOrderWithStatusesSpecification(int orderId) 
        : base(mo => mo.Id == orderId)
    {
        this.AddInclude(mo => mo.Statuses);
    }

    public MaintenanceOrderWithStatusesSpecification(int machineId, bool includeStatus = true)
        : base(mo => mo.MachineId == machineId)
    {
        if (includeStatus)
        {
            this.AddInclude(mo => mo.Statuses);
        }
    }
}
