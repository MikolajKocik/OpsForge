using OpsForge.Domain.SeedWork;
using System.Linq.Expressions;

namespace OpsForge.Domain.Entities.AggregateMachine;

public sealed class MachineWithMaintenanceSpecification : BaseSpecification<Machine>
{
    public MachineWithMaintenanceSpecification(Guid machineCode) 
        : base(m => m.Code == machineCode)
    {
        this.AddInclude(m => m.Maintenances);
    }

    public MachineWithMaintenanceSpecification(Enums.Status machineStatus)
        : base(m => m.MachineStatus == machineStatus) 
    {
        this.AddInclude(m => m.Maintenances);
    }
}
