using OpsForge.Domain.Entities.AggregateMachine.Machines;

namespace OpsForge.Application.DTOs;

public sealed class PartsSnapshot
{
    public IReadOnlyCollection<AutomaticMeldingMachine> Automatic { get; init; }
    public IReadOnlyCollection<CncMillingMachine> Cnc { get; init; }
    public IReadOnlyCollection<HydraulicPress> Hydraulic { get; init; }
    public IReadOnlyCollection<InjectionMeldingMachine> Injection { get; init; }
    public IReadOnlyCollection<RoboticAssemblyLine> Robotic { get; init; }
}
