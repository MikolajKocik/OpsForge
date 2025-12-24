using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines;

public sealed class HydraulicPress : Machine
{
    public SparePart? HydraulicPump { get; private set; } 
    public SparePart? HydraulicCylinder { get; private set; } 
    public SparePart? HydraulicOilTank { get; private set; }
    public SparePart? ProportionalValves { get; private set; }

    public HydraulicPress() : base() { }

    public HydraulicPress(
        Line productionLine,
        MachineSpecification specification,
        SparePart? hydraulicPump = null,
        SparePart? hydraulicCylinder = null,
        SparePart? hydraulicOilTank = null,
        SparePart? proportionalValves = null
        ) : base(nameof(HydraulicPress), productionLine, specification) 
    {
        this.HydraulicPump = hydraulicPump;
        this.HydraulicCylinder = hydraulicCylinder;
        this.HydraulicOilTank = hydraulicOilTank;
        this.ProportionalValves = proportionalValves;
    }

    public enum HydrauliPartType
    {
        HydraulicPump,
        HydraulicCylinder,
        HydraulicOilTank,
        ProportionalValves
    }

    public void ReplacePart(HydrauliPartType partType, SparePart newPartName)
    {
        _ = partType switch
        {
            HydrauliPartType.HydraulicPump => this.HydraulicPump = newPartName,
            HydrauliPartType.HydraulicCylinder => this.HydraulicCylinder = newPartName,
            HydrauliPartType.HydraulicOilTank => this.HydraulicOilTank = newPartName,
            HydrauliPartType.ProportionalValves => this.ProportionalValves = newPartName,
            _ => throw new ArgumentOutOfRangeException(nameof(newPartName))
        };
    }
}
