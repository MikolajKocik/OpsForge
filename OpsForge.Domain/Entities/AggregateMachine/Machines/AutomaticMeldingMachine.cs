
using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines
{
    public sealed class AutomaticMeldingMachine : Machine
    {
        public string ControlUnit { get; private set; } = string.Empty;
        public string ConveyorSystem { get; private set; } = string.Empty;
        public string Sensors { get; private set; } = string.Empty;
        public string Actuators { get; private set; } = string.Empty;
        public string SafetyGuards { get; private set; } = string.Empty;

        public string WeldingHead { get; private set; } = string.Empty;
        public string CoolingSystem { get; private set; } = string.Empty;
        public string ToolChanger { get; private set; } = string.Empty;
        public string PLC { get; private set; } = string.Empty;
        public string HMI { get; private set; } = string.Empty;

        public AutomaticMeldingMachine(
            Guid code,
            string name,
            Line productionLine,
            MachineSpecification specification
            ) : base(code, nameof(AutomaticMeldingMachine), productionLine, specification) { }

        public enum AutomaticPart
        {
            ControlUnit,
            ConveyorSystem,
            Sensors,
            Actuators,
            SafetyGuards,
            WeldingHead,
            CoolingSystem,
            ToolChanger,
            PLC,
            HMI
        }

        public void ReplacePart(AutomaticPart partType, string newPartName)
        {
            switch (partType)
            {
                case AutomaticPart.ControlUnit:
                    ControlUnit = newPartName;
                    break;
                case AutomaticPart.ConveyorSystem:
                    ConveyorSystem = newPartName;
                    break;
                case AutomaticPart.Sensors:
                    Sensors = newPartName;
                    break;
                case AutomaticPart.Actuators:
                    Actuators = newPartName;
                    break;
                case AutomaticPart.SafetyGuards:
                    SafetyGuards = newPartName;
                    break;
                case AutomaticPart.WeldingHead:
                    WeldingHead = newPartName;
                    break;
                case AutomaticPart.CoolingSystem:
                    CoolingSystem = newPartName;
                    break;
                case AutomaticPart.ToolChanger:
                    ToolChanger = newPartName;
                    break;
                case AutomaticPart.PLC:
                    PLC = newPartName;
                    break;
                case AutomaticPart.HMI:
                    HMI = newPartName;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(partType), $"Undefined part type: {partType}.");
            }
        }
    }
}
