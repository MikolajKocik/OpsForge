using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines;

public sealed class RoboticAssemblyLine : Machine
{
   public string RobotArmJoint { get; private set; } = string.Empty;
   public string ConveyorBelt { get; private set; } = string.Empty;
   public string VisionSensors { get; private set; } = string.Empty;
   public string PneumaticGrippers { get; private set; } = string.Empty;
   public string PLCHMIController { get; private set; } = string.Empty;

    public RoboticAssemblyLine(
        Guid code,
        Line productionLine,
        MachineSpecification specification
        ) : base(code, nameof(RoboticAssemblyLine), productionLine, specification) { }

    public enum RoboticPart
    {
        RobotArmJoint,
        ConveyorBelt,
        VisionSensors,
        PneumaticGrippers,
        PLCHMIController
    }

    public void ReplacePart(RoboticPart partType, string newPartName)
    {
        switch (partType)
        {
            case RoboticPart.RobotArmJoint:
                this.RobotArmJoint = newPartName;
                break;

            case RoboticPart.ConveyorBelt:
                this.ConveyorBelt = newPartName;
                break;

            case RoboticPart.VisionSensors:
                this.VisionSensors = newPartName;
                break;

            case RoboticPart.PneumaticGrippers:
                this.PneumaticGrippers = newPartName;
                break;

            case RoboticPart.PLCHMIController:
                this.PLCHMIController = newPartName;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(partType),
                    $"Undefined part type: {partType}.");
        }
    }
}
