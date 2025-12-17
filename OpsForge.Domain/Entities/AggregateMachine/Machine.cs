using OpsForge.Domain.Entities.AggregateMachine;
using OpsForge.Domain.Entities.AggregateMachine.Inventory;
using OpsForge.Domain.Entities.AggregateMaintenance;
using OpsForge.Domain.Entities.Base;
using OpsForge.Domain.Enums;
using OpsForge.Domain.Exceptions;
using OpsForge.Domain.SeedWork.Interfaces;

namespace OpsForge.Domain.Entities;

public class Machine : BaseEntity, IAggregateRoot
{
    public Guid Code { get; private set; }
    public string Name { get; private set; }
    public Line ProductionLine { get; private set; }
    public Inventory Inventory { get; private set; }

    public Status MachineStatus { get; private set; }
    public MachineSpecification Specification { get; private set; }

    private readonly List<MaintenanceOrder> _maintenances = new();
    public IReadOnlyCollection<MaintenanceOrder> Maintenances => 
        this._maintenances.AsReadOnly();

    public Machine(Guid code, string name, Line productionLine, MachineSpecification specification)
    {
        if (code == Guid.Empty)
            throw new ArgumentException("Machine's code is required");

        this.Specification = specification;
        this.Code = code;
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.ProductionLine = productionLine ?? throw new ArgumentNullException(nameof(productionLine));

        this.MachineStatus = Status.Open;
        this.Inventory = new Inventory();
    }

    public void SetStatus(Status newStatus)
    {
        if (this.MachineStatus == Status.Completed && newStatus == Status.Cancelled)
            throw new CannotCancelCompletedMachineException("Machine cannot be cancelled because it is completed.");

        if (this.MachineStatus == Status.Cancelled && newStatus == Status.Completed)
            throw new AlreadyCompletedMachineException("Machine has already completed status");

        this.MachineStatus = newStatus;
    }

    public void AddMaintenanceOrder(params MaintenanceOrder[] orders)
    {
        if (orders.Length == 0 || !orders.Any())
            return;

        foreach (var order in orders)
        {
            this._maintenances.Add(order);
        }
    }
}
