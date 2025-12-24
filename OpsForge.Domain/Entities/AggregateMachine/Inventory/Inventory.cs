using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.SeedWork;
using System.Collections.Generic;

namespace OpsForge.Domain.Entities.AggregateMachine.Inventory;

/// <summary>
/// Aggregate inventory stores the whole production parts in warehouse
/// </summary>
public sealed class Inventory : ValueObject
{
    /// <summary>
    /// List of automatic parts
    /// </summary>
    private readonly List<AutomaticMeldingMachine> _automaticParts = new();
    public IReadOnlyCollection<AutomaticMeldingMachine> AutomaticParts => _automaticParts.AsReadOnly();

    /// <summary>
    /// List of cnc parts
    /// </summary>
    private readonly List<CncMillingMachine> _cncParts = new();
    public IReadOnlyCollection<CncMillingMachine> CncParts => _cncParts.AsReadOnly();

    /// <summary>
    /// List of hydraulic parts
    /// </summary>
    private readonly List<HydraulicPress> _hydraulicParts = new();
    public IReadOnlyCollection<HydraulicPress> HydraulicParts => _hydraulicParts.AsReadOnly();

    /// <summary>
    /// List of injection parts
    /// </summary>
    public readonly List<InjectionMeldingMachine> _injectionParts = new();
    public IReadOnlyCollection<InjectionMeldingMachine> InjectionParts => _injectionParts.AsReadOnly();

    /// <summary>
    /// List of robotic parts
    /// </summary>
    private readonly List<RoboticAssemblyLine> _roboticParts = new();
    public IReadOnlyCollection<RoboticAssemblyLine> RoboticParts => _roboticParts.AsReadOnly();


    // hashset for manipulating child machine parts
    private readonly HashSet<object> _parts = new HashSet<object>();
    public IReadOnlyCollection<object> Parts => this._parts;

    public void RemovePart(params string[] parts)
    {
        foreach (var part in parts)
        {
            if (this._parts.Contains(part))
            {
                this._parts.Remove(part);
            }
        }
    }

    public void AddPart(params SparePart[] parts)
    {
        foreach (var part in parts)
        {
            if (part is null) continue;

            if (!this._parts.Contains(part))
            {
                this._parts.Add(part);
            }
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this._parts;
    }
}