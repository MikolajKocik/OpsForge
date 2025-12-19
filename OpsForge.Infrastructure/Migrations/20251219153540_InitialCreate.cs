using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpsForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Machines");

            migrationBuilder.CreateTable(
                name: "Machine",
                schema: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionLine = table.Column<int>(type: "int", nullable: false),
                    MachineStatus = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dimension_Height = table.Column<double>(type: "float", nullable: false),
                    Dimension_Length = table.Column<double>(type: "float", nullable: false),
                    Dimension_Width = table.Column<double>(type: "float", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PowerKw = table.Column<double>(type: "float", nullable: false),
                    Voltage = table.Column<double>(type: "float", nullable: false),
                    WeightKg = table.Column<double>(type: "float", nullable: false),
                    MachineType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Actuators_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ControlUnit_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConveyorSystem_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CoolingSystem_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HMI_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PLC_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SafetyGuards_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sensors_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToolChanger_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WeldingHead_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Actuators_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Actuators_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Actuators_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControlUnit_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControlUnit_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControlUnit_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConveyorSystem_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConveyorSystem_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConveyorSystem_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoolingSystem_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoolingSystem_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoolingSystem_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HMI_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HMI_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HMI_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLC_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLC_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLC_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SafetyGuards_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SafetyGuards_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SafetyGuards_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sensors_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sensors_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sensors_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolChanger_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolChanger_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolChanger_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingHead_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingHead_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingHead_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BallScrews_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CoolantPump_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LinearGuides_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MainSpindle_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToolMagazine_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BallScrews_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BallScrews_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BallScrews_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoolantPump_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoolantPump_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoolantPump_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinearGuides_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinearGuides_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinearGuides_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainSpindle_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainSpindle_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainSpindle_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolMagazine_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolMagazine_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolMagazine_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HydraulicCylinder_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HydraulicOilTank_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HydraulicPress_HydraulicPump_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProportionalValves_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HydraulicCylinder_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HydraulicCylinder_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HydraulicCylinder_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HydraulicOilTank_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HydraulicOilTank_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HydraulicOilTank_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HydraulicPump_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HydraulicPump_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HydraulicPump_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProportionalValves_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProportionalValves_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProportionalValves_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClampingUnit_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FumeExtractionSystem_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HydraulicPump_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InjectionMold_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InjectionUnit_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaterialHopperDryer_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PositionerTable_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PowerSourceWelder_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WeldingTorch_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WireFeeder_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClampingUnit_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClampingUnit_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClampingUnit_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FumeExtractionSystem_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FumeExtractionSystem_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FumeExtractionSystem_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InjectionMold_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InjectionMold_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InjectionMold_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InjectionUnit_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InjectionUnit_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InjectionUnit_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialHopperDryer_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialHopperDryer_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialHopperDryer_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionerTable_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionerTable_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionerTable_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerSourceWelder_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerSourceWelder_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerSourceWelder_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingTorch_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingTorch_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeldingTorch_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireFeeder_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireFeeder_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireFeeder_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConveyorBelt_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PLCHMIController_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PneumaticGrippers_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RobotArmJoint_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisionSensors_LastReplaced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConveyorBelt_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConveyorBelt_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConveyorBelt_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLCHMIController_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLCHMIController_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PLCHMIController_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PneumaticGrippers_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PneumaticGrippers_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PneumaticGrippers_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RobotArmJoint_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RobotArmJoint_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RobotArmJoint_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisionSensors_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisionSensors_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisionSensors_SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inventory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.Id);
                    table.UniqueConstraint("AK_Machine_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceOrder",
                schema: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    Schedule_MaintenanceInterval = table.Column<TimeSpan>(type: "time", nullable: false),
                    Schedule_LastMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Schedule_Type = table.Column<int>(type: "int", nullable: false),
                    Schedule_Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceOrder_Machine_MachineId",
                        column: x => x.MachineId,
                        principalSchema: "Machines",
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceStatus",
                schema: "Machines",
                columns: table => new
                {
                    MaintenanceStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceOrderId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceStatus", x => x.MaintenanceStatusId);
                    table.ForeignKey(
                        name: "FK_MaintenanceStatus_MaintenanceOrder_MaintenanceOrderId",
                        column: x => x.MaintenanceOrderId,
                        principalSchema: "Machines",
                        principalTable: "MaintenanceOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceOrder_MachineId",
                schema: "Machines",
                table: "MaintenanceOrder",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceStatus_MaintenanceOrderId",
                schema: "Machines",
                table: "MaintenanceStatus",
                column: "MaintenanceOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceStatus",
                schema: "Machines");

            migrationBuilder.DropTable(
                name: "MaintenanceOrder",
                schema: "Machines");

            migrationBuilder.DropTable(
                name: "Machine",
                schema: "Machines");
        }
    }
}
