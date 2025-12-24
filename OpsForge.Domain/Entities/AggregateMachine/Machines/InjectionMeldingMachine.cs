using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines;

public sealed class InjectionMeldingMachine : Machine
{
    public SparePart? PowerSourceWelder { get; private set; } 
    public SparePart? WeldingTorch { get; private set; } 
    public SparePart? WireFeeder { get; private set; } 
    public SparePart? FumeExtractionSystem { get; private set; } 
    public SparePart? PositionerTable { get; private set; } 

    public SparePart? InjectionUnit { get; private set; } 
    public SparePart? HydraulicPump { get; private set; } 
    public SparePart? InjectionMold { get; private set; } 
    public SparePart? ClampingUnit { get; private set; } 
    public SparePart? MaterialHopperDryer { get; private set; }

    public InjectionMeldingMachine() : base() { }

    public InjectionMeldingMachine(
        string name,
        Line productionLine,
        MachineSpecification specification,
        SparePart? powerSourceWelder = null,
        SparePart? weldingTorch = null,
        SparePart? wireFeeder = null,
        SparePart? fumeExtractionSystem = null,
        SparePart? positionerTable = null,
        SparePart? injectionUnit = null,
        SparePart? hydraulicPump = null,
        SparePart? injectionMold = null,
        SparePart? clampingUnit = null,
        SparePart? materialHopperDryer = null
        ) : base(nameof(InjectionMeldingMachine), productionLine, specification) 
    {
        this.PowerSourceWelder = powerSourceWelder;
        this.WeldingTorch = weldingTorch;
        this.WireFeeder = wireFeeder;
        this.FumeExtractionSystem = fumeExtractionSystem;
        this.PositionerTable = positionerTable;
        this.InjectionUnit = injectionUnit;
        this.HydraulicPump = hydraulicPump;
        this.InjectionMold = injectionMold;
        this.ClampingUnit = clampingUnit;
        this.MaterialHopperDryer = materialHopperDryer;
    }

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

    public void ReplacePart(InjectionPart partType, SparePart? newPartName)
    {
        _ = partType switch
        {
            InjectionPart.PowerSourceWelder => this.PowerSourceWelder = newPartName,
            InjectionPart.WeldingTorch => this.WeldingTorch = newPartName,
            InjectionPart.WireFeeder => this.WireFeeder = newPartName,
            InjectionPart.FumeExtractionSystem => this.FumeExtractionSystem = newPartName,
            InjectionPart.PositionerTable => this.PositionerTable = newPartName,
            InjectionPart.InjectionUnit => this.InjectionUnit = newPartName,
            InjectionPart.HydraulicPump => this.HydraulicPump = newPartName,
            InjectionPart.InjectionMold => this.InjectionMold = newPartName,
            InjectionPart.ClampingUnit => this.ClampingUnit = newPartName,
            InjectionPart.MaterialHopperDryer => this.MaterialHopperDryer = newPartName,
            _ => throw new ArgumentOutOfRangeException(nameof(newPartName))
        };
    }
}
