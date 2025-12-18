using OpsForge.Domain.SeedWork;

namespace OpsForge.Domain.Entities.AggregateMachine;

public sealed class MachineWithInventoryPartsSpecification : BaseSpecification<Machine>
{
    public MachineWithInventoryPartsSpecification(string name) : base(m => m.Name == name)
    {
        this.AddInclude(m => m.Inventory);
        
        this.AddInclude("Inventory.AutomaticParts");
        this.AddInclude("Inventory.RoboticParts");
        this.AddInclude("Inventory.InjectionParts");
        this.AddInclude("Inventory.CncParts");
        this.AddInclude("Inventory.HydraulicParts");
    }
}
