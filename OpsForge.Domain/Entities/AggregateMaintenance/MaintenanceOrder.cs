using OpsForge.Domain.Entities.Base;
using OpsForge.Domain.Enums;
using OpsForge.Domain.SeedWork.Interfaces;

namespace OpsForge.Domain.Entities.AggregateMaintenance;

public sealed class MaintenanceOrder : BaseEntity, IAggregateRoot
{
    private List<Status> _statuses = new();
    public IReadOnlyCollection<Status> Statuses
        => this._statuses.AsReadOnly();

    public int MachineId { get; }

    public MaintenanceSchedule Schedule { get; private set; }

    public MaintenanceOrder() { }

    public MaintenanceOrder(int machineId, MaintenanceSchedule schedule)
    {
        this.MachineId = machineId;
        this.Schedule = schedule;
    }

    public void AddMaintenanceOrderStatus(Status status)
    {
        this._statuses.Add(status);
    }
}
