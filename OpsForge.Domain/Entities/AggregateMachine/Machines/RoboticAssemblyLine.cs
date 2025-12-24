using OpsForge.Domain.Enums;

namespace OpsForge.Domain.Entities.AggregateMachine.Machines;

public sealed class RoboticAssemblyLine : Machine
{
    public SparePart? RobotArmJoint { get; private set; }
    public SparePart? ConveyorBelt { get; private set; }
    public SparePart? VisionSensors { get; private set; }
    public SparePart? PneumaticGrippers { get; private set; }
    public SparePart? PLCHMIController { get; private set; }

    public RoboticAssemblyLine() : base() { }

    public RoboticAssemblyLine(
        Line productionLine,
        MachineSpecification specification,
        SparePart? robotArmJoint = null,
        SparePart? conveyorBelt = null,
        SparePart? visionSensors = null,
        SparePart? pneumaticGrippers = null,
        SparePart? plchmiController = null
        ) : base(nameof(RoboticAssemblyLine), productionLine, specification)
    {
        this.RobotArmJoint = robotArmJoint;
        this.ConveyorBelt = conveyorBelt;
        this.VisionSensors = visionSensors;
        this.PneumaticGrippers = pneumaticGrippers;
        this.PLCHMIController = plchmiController;
    }

    public enum RoboticPart
    {
        RobotArmJoint,
        ConveyorBelt,
        VisionSensors,
        PneumaticGrippers,
        PLCHMIController
    }

    public void ReplacePart(RoboticPart partType, SparePart? newPartName)
    {
        _ = partType switch
        {
            RoboticPart.RobotArmJoint => this.RobotArmJoint = newPartName,
            RoboticPart.ConveyorBelt => this.ConveyorBelt = newPartName,
            RoboticPart.VisionSensors => this.VisionSensors = newPartName,
            RoboticPart.PneumaticGrippers => this.PneumaticGrippers = newPartName,
            RoboticPart.PLCHMIController => this.PLCHMIController = newPartName,
            _ => throw new ArgumentOutOfRangeException(nameof(newPartName))
        };
    }
}
