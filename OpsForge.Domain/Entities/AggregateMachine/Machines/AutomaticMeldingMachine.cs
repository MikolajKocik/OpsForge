
using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines
{
    public sealed class AutomaticMeldingMachine : Machine
    {
        public SparePart? ControlUnit { get; private set; } 
        public SparePart? ConveyorSystem { get; private set; } 
        public SparePart? Sensors { get; private set; } 
        public SparePart? Actuators { get; private set; } 
        public SparePart? SafetyGuards { get; private set; } 

        public SparePart? WeldingHead { get; private set; } 
        public SparePart? CoolingSystem { get; private set; } 
        public SparePart? ToolChanger { get; private set; } 
        public SparePart? PLC { get; private set; }
        public SparePart? HMI { get; private set; }

        public AutomaticMeldingMachine() { }

        public AutomaticMeldingMachine(
            string name,
            Line productionLine,
            MachineSpecification specification,
            SparePart? controlUnit = null,
            SparePart? conveyorSystem = null,
            SparePart? sensors = null,
            SparePart? actuators = null,
            SparePart? safetyGuards = null,
            SparePart? weldingHead = null,
            SparePart? coolingSystem = null,
            SparePart? toolChanger = null,
            SparePart? plc = null,
            SparePart? hmi = null
            ) : base(nameof(AutomaticMeldingMachine), productionLine, specification) 
        {
            this.ControlUnit = controlUnit;
            this.ConveyorSystem = conveyorSystem;
            this.Sensors = sensors;
            this.Actuators = actuators;
            this.SafetyGuards = safetyGuards;
            this.WeldingHead = weldingHead;
            this.CoolingSystem = coolingSystem;
            this.ToolChanger = toolChanger;
            this.PLC = plc;
            this.HMI = hmi;
        }

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

        public void ReplacePart(AutomaticPart partType, SparePart newPartName)
        {
            _ = partType switch
            {
                AutomaticPart.ControlUnit => this.ControlUnit = newPartName,
                AutomaticPart.ConveyorSystem => this.ConveyorSystem = newPartName,
                AutomaticPart.Sensors => this.Sensors = newPartName,
                AutomaticPart.Actuators => this.Actuators = newPartName,
                AutomaticPart.SafetyGuards => this.SafetyGuards = newPartName,
                AutomaticPart.WeldingHead => this.WeldingHead = newPartName,
                AutomaticPart.CoolingSystem => this.CoolingSystem = newPartName,
                AutomaticPart.ToolChanger => this.ToolChanger = newPartName,
                AutomaticPart.PLC => this.PLC = newPartName,
                AutomaticPart.HMI => this.HMI = newPartName,
                _ => throw new ArgumentOutOfRangeException(nameof(partType))
            };
        }
    }
}
