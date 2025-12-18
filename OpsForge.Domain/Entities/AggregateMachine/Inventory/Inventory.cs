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
    public IReadOnlyCollection<AutomaticMeldingMachine> AutomaticParts => this._automaticParts.AsReadOnly();

    /// <summary>
    /// List of cnc parts
    /// </summary>
    private readonly List<CncMillingMachine> _cncParts = new();
    public IReadOnlyCollection<CncMillingMachine> CncParts => this._cncParts.AsReadOnly();

    /// <summary>
    /// List of hydraulic parts
    /// </summary>
    private readonly List<HydraulicPress> _hydraulicParts = new();
    public IReadOnlyCollection<HydraulicPress> HydraulicParts => this._hydraulicParts.AsReadOnly();

    /// <summary>
    /// List of injection parts
    /// </summary>
    private readonly List<InjectionMeldingMachine> _injectionParts = new();
    public IReadOnlyCollection<InjectionMeldingMachine> InjectionParts => this._injectionParts.AsReadOnly();

    /// <summary>
    /// List of robotic parts
    /// </summary>
    private readonly List<RoboticAssemblyLine> _roboticParts = new();
    public IReadOnlyCollection<RoboticAssemblyLine> RoboticParts => this._roboticParts.AsReadOnly();


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

    public void AddPart(params string[] parts)
    {
        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part)) continue;

            if (!this._parts.Contains(part))
            {
                this._parts.Add(part);
            }
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.AutomaticParts;
        yield return this.CncParts;
        yield return this.HydraulicParts;
        yield return this.InjectionParts;
        yield return this.RoboticParts;
    }
}