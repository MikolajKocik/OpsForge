using OpsForge.Application.DTOs;
using OpsForge.Domain.Entities.AggregateMachine.Inventory;

namespace OpsForge.Application.Utilities;

public sealed class MachineUtility
{

    public PartsSnapshot GetParts(Inventory inventory)
    {
        return new PartsSnapshot
        {
            Automatic = inventory.AutomaticParts.ToList(),
            Robotic = inventory.RoboticParts.ToList(),
            Injection = inventory.InjectionParts.ToList(),
            Cnc = inventory.CncParts.ToList(),
            Hydraulic = inventory.HydraulicParts.ToList()
        };
    }
}
