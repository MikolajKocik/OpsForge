using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine;
using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.Enums;
using OpsForge.Infrastructure.Persistence.Contexts;

namespace OpsForge.Infrastructure.Persistence.InitialData;

public sealed class DbInitializer
{
    public static async Task SeedAsync(MachineContext context)
    {
        if (context.Machines.Any()) return;

        var largeDim = new Dimensions(3500.0, 2500.0, 2800.0);
        var smallDim = new Dimensions(1200.0, 800.0, 1500.0);

        // some spare parts
        var plcS7 = new SparePart("Siemens", "S7-1200", "SN-PLC-001");
        var spindleHsd = new SparePart("HSD", "ES929", "SN-SPINDLE-99");
        var pumpRexroth = new SparePart("Bosch Rexroth", "A10VSO", "SN-PUMP-55");
        var sensorKeyence = new SparePart("Keyence", "LR-Z", "SN-SENS-10");

        var machines = new List<Machine>
        {
            // CNC Milling Machine
            new CncMillingMachine(
                Line.M1, 
                new MachineSpecification(
                    "VMC-850", "Mazak", 25.0, 400.0, 5500.0,
                    largeDim, "Cast Iron", "High precision milling center"),
                mainSpindle: spindleHsd,
                coolantPump: new SparePart("Grundfos", "MTR-15")
            ),

            // Robotic Assembly Line
            new RoboticAssemblyLine(
                Line.M2,
                new MachineSpecification(
                    "KR-210", "KUKA", 15.0, 400.0, 1200.0,
                    largeDim, "Aluminium/Steel", "Welding line robot"),
                robotArmJoint: new SparePart("KUKA", "Joint-Axis-1", "SN-K-01"),
                plchmiController: plcS7
            ),

            // Hydraulic Press
            new HydraulicPress(
                Line.M3,
                new MachineSpecification(
                    "HP-500", "Schuler", 45.0, 400.0, 12000.0,
                    largeDim, "Steel", "Heavy duty hydraulic press"),
                hydraulicPump: pumpRexroth,
                hydraulicCylinder: new SparePart("Parker", "Series 2H")
            ),

            // Automatic Melding Machine
            new AutomaticMeldingMachine(
                "Molder-Alpha-1",
                Line.M1,
                new MachineSpecification(
                    "AM-200", "Engel", 30.0, 400.0, 4500.0,
                    smallDim, "Composite", "Automated plastic melding unit"),
                plc: plcS7,
                hmi: new SparePart("Beijer", "X2 Pro 7")
            )
        };

        machines[0].Inventory.AddPart(sensorKeyence, plcS7);
        machines[1].Inventory.AddPart(new SparePart("SMC", "Pneumatic Valve"));

        context.Machines.AddRange(machines);
        await context.SaveChangesAsync();
    }
}
