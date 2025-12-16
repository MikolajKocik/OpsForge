using OpsForge.Domain.Entities.Base;
using OpsForge.Domain.Enums;
using OpsForge.Domain.SeedWork.Interfaces;

namespace OpsForge.Domain.Entities.AggregateMaintenance;

public sealed class MaintenanceOrder : BaseEntity, IAggregateRoot
{
    private List<Status> _statuses = new();
    public IReadOnlyCollection<Status> Statuses
        => _statuses.AsReadOnly();

    public int MachineId { get; }

    public MaintenanceOrder(int machineId)
    {
        MachineId = machineId;
    }

    public void AddMaintenanceOrderStatus(Status status)
    {
        _statuses.Add(status);
    }
}
