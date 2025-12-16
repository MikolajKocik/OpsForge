using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines;

public sealed class InjectionMeldingMachine : Machine
{
    public string PowerSourceWelder { get; private set; } = string.Empty;
    public string WeldingTorch { get; private set; } = string.Empty;
    public string WireFeeder { get; private set; } = string.Empty;
    public string FumeExtractionSystem { get; private set; } = string.Empty;
    public string PositionerTable { get; private set; } = string.Empty;

    public string InjectionUnit { get; private set; } = string.Empty;
    public string HydraulicPump { get; private set; } = string.Empty;
    public string InjectionMold { get; private set; } = string.Empty;
    public string ClampingUnit { get; private set; } = string.Empty;
    public string MaterialHopperDryer { get; private set; } = string.Empty;

    public InjectionMeldingMachine(
        Guid code,
        string name,
        Line productionLine,
        MachineSpecification specification
        ) : base(code, nameof(InjectionMeldingMachine), productionLine, specification) { }

    public enum InjectionPart
    {
        PowerSourceWelder,
        WeldingTorch,
        WireFeeder,
        FumeExtractionSystem,
        PositionerTable,
        InjectionUnit,
        HydraulicPump,
        InjectionMold,
        ClampingUnit,
        MaterialHopperDryer
    }

    public void ReplacePart(InjectionPart partType, string newPartName)
    {
        switch (partType)
        {
            case InjectionPart.PowerSourceWelder:
                PowerSourceWelder = newPartName;
                break;
            case InjectionPart.WeldingTorch:
                WeldingTorch = newPartName;
                break;
            case InjectionPart.WireFeeder:
                WireFeeder = newPartName;
                break;
            case InjectionPart.FumeExtractionSystem:
                FumeExtractionSystem = newPartName;
                break;
            case InjectionPart.PositionerTable:
                PositionerTable = newPartName;
                break;
            case InjectionPart.InjectionUnit:
                InjectionUnit = newPartName;
                break;
            case InjectionPart.HydraulicPump:
                HydraulicPump = newPartName;
                break;
            case InjectionPart.InjectionMold:
                InjectionMold = newPartName;
                break;
            case InjectionPart.ClampingUnit:
                ClampingUnit = newPartName;
                break;
            case InjectionPart.MaterialHopperDryer:
                MaterialHopperDryer = newPartName;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(partType), $"Undefined part type: {partType}.");
        }
    }
}
