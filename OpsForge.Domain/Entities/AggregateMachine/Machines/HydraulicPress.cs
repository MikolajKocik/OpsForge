using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines;

public sealed class HydraulicPress : Machine
{
    public string HydraulicPump { get; private set; } = string.Empty;
    public string HydraulicCylinder { get; private set; } = string.Empty;
    public string HydraulicOilTank { get; private set; } = string.Empty;
    public string ProportionalValves { get; private set; } = string.Empty;

    public HydraulicPress(
        Guid code,
        Line productionLine,
        MachineSpecification specification
        ) : base(code, nameof(HydraulicPress), productionLine, specification) { }

    public enum HydrauliPartType
    {
        HydraulicPump,
        HydraulicCylinder,
        HydraulicOilTank,
        ProportionalValves
    }

    public void ReplacePart(HydrauliPartType partType, string newPartName)
    {
        switch (partType)
        {
            case HydrauliPartType.HydraulicPump:
                this.HydraulicPump = newPartName;
                break;

            case HydrauliPartType.HydraulicCylinder:
                this.HydraulicCylinder = newPartName;
                break;

            case HydrauliPartType.HydraulicOilTank:
                this.HydraulicOilTank = newPartName;
                break;

            case HydrauliPartType.ProportionalValves:
                this.ProportionalValves = newPartName;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(partType),
                    $"Undefined part type: {partType}.");
        }
    }
}
