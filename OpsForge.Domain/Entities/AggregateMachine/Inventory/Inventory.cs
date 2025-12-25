using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.SeedWork;

namespace OpsForge.Domain.Entities.AggregateMachine.Inventory;

/// <summary>
/// Aggregate inventory stores the whole production parts in warehouse
/// </summary>
public sealed class Inventory : ValueObject
{
    public List<CncMillingMachine> CncParts { get; private set; } = new();
    public List<RoboticAssemblyLine> RoboticParts { get; private set; } = new();
    public List<AutomaticMeldingMachine> AutomaticParts { get; private set; } = new();
    public List<HydraulicPress> HydraulicParts { get; private set; } = new();
    public List<InjectionMeldingMachine> InjectionParts { get; private set; } = new();

    private List<SparePart?> _parts =>
           (List<SparePart?>)this.CncParts.SelectMany(m => new[] { m.MainSpindle, m.BallScrews, m.ToolMagazine, m.LinearGuides, m.CoolantPump })
        .Concat(this.RoboticParts
            .SelectMany(m => new[] { m.RobotArmJoint, m.ConveyorBelt, m.VisionSensors, m.PneumaticGrippers, m.PLCHMIController }))
        .Concat(this.AutomaticParts
            .SelectMany(m => new[] { m.ControlUnit, m.ConveyorSystem, m.Sensors, m.Actuators, m.SafetyGuards, m.WeldingHead, m.CoolingSystem, m.ToolChanger, m.PLC, m.HMI }))
        .Concat(this.HydraulicParts
            .SelectMany(m => new[] { m.HydraulicPump, m.HydraulicCylinder, m.HydraulicOilTank, m.ProportionalValves }))
        .Concat(this.InjectionParts
            .SelectMany(m => new[] { m.PowerSourceWelder, m.WeldingTorch, m.WireFeeder, m.FumeExtractionSystem, m.PositionerTable, m.InjectionUnit, m.HydraulicPump, m.InjectionMold, m.ClampingUnit, m.MaterialHopperDryer })
    );

    public IEnumerable<SparePart?> Parts => this._parts;

    public void AddPart(params SparePart?[] newPart)
    {
        foreach(var part in newPart)
        {
            if (part is null) continue;

            if (!this._parts.Contains(part))
            {
                this._parts.Add(part);
            }
        }
    }

    public void RemovePart(params SparePart?[] partToRemove)
    {
        foreach(var part in partToRemove)
        {
            if (part is null) continue; 

            if(this._parts.Contains(part))
            {
                this._parts.Remove(part);
            }
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.CncParts;
        yield return this.AutomaticParts;
        yield return this.RoboticParts;
        yield return this.InjectionParts;
        yield return this.HydraulicParts;
    }
}