using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines;

public sealed class CncMillingMachine : Machine
{
    public SparePart? MainSpindle { get; private set; }
    public SparePart? BallScrews { get; private set; }
    public SparePart? ToolMagazine { get; private set; }
    public SparePart? LinearGuides { get; private set; }
    public SparePart? CoolantPump { get; private set; }

    public CncMillingMachine(
        Line productionLine,
        MachineSpecification specification,
        SparePart? mainSpindle = null,
        SparePart? ballScrews = null,
        SparePart? toolMagazine = null,
        SparePart? linearGuides = null,
        SparePart? coolantPump = null
        ) : base(nameof(CncMillingMachine), productionLine, specification)
    {
        this.MainSpindle = mainSpindle;
        this.BallScrews = ballScrews;
        this.ToolMagazine = toolMagazine;
        this.LinearGuides = linearGuides;
        this.CoolantPump = coolantPump;
    }

    public enum CncPartType
    {
        MainSpindle,
        BallScrews,
        ToolMagazine,
        LinearGuides,
        CoolantPump,
    }

    public void ReplacePart(CncPartType partType, SparePart newPartName)
    {
        _ = partType switch
        {
            CncPartType.MainSpindle => this.MainSpindle = newPartName,
            CncPartType.BallScrews => this.BallScrews = newPartName,
            CncPartType.ToolMagazine => this.ToolMagazine = newPartName,
            CncPartType.LinearGuides => this.LinearGuides = newPartName,
            CncPartType.CoolantPump => this.CoolantPump = newPartName,
            _ => throw new ArgumentOutOfRangeException(nameof(newPartName))
        };
    }
}
