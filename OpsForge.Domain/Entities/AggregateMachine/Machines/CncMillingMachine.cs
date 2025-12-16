using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines;

public sealed class CncMillingMachine : Machine
{
    public string MainSpindle { get; private set; } = string.Empty;
    public string BallScrews { get; private set; } = string.Empty;
    public string ToolMagazine { get; private set; } = string.Empty;
    public string LinearGuides { get; private set; } = string.Empty;
    public string CoolantPump { get; private set; } = string.Empty;

    public CncMillingMachine(
        Guid code,
        Line productionLine,
        MachineSpecification specification
        ) : base(code, nameof(CncMillingMachine), productionLine, specification) { }

    public enum CncPartType
    {
        MainSpindle,
        BallScrews,
        ToolMagazine,
        LinearGuides,
        CoolantPump,
    }

    public void ReplacePart(CncPartType partType, string newPartName)
    {
        switch (partType)
        {
            case CncPartType.MainSpindle:
                this.MainSpindle = newPartName;
                break;

            case CncPartType.BallScrews:
                this.BallScrews = newPartName;
                break;

            case CncPartType.ToolMagazine:
                this.ToolMagazine = newPartName;
                break;

            case CncPartType.LinearGuides:
                this.LinearGuides = newPartName;
                break;

            case CncPartType.CoolantPump:
                this.CoolantPump = newPartName;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(partType),
                    $"Undefined part type: {partType}.");
        }
    }
}
   