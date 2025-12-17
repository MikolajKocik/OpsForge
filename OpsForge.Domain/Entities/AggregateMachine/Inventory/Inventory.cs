using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.SeedWork;

namespace OpsForge.Domain.Entities.AggregateMachine.Inventory;

/// <summary>
/// Aggregate inventory stores the whole production parts in warehouse
/// </summary>
public sealed class Inventory : ValueObject
{
    /// <summary>
    /// List of automatic parts
    /// </summary>
    private List<AutomaticMeldingMachine> _automaticParts { get; } = new();
    public IReadOnlyCollection<AutomaticMeldingMachine> AutomaticParts => _automaticParts.AsReadOnly();

    /// <summary>
    /// List of cnc parts
    /// </summary>
    private List<CncMillingMachine> _cncParts { get; } = new();
    public IReadOnlyCollection<CncMillingMachine> CncParts => _cncParts.AsReadOnly();

    /// <summary>
    /// List of hydraulic parts
    /// </summary>
    private List<HydraulicPress> _hydraulicParts { get; } = new();
    public IReadOnlyCollection<HydraulicPress> HydraulicParts => _hydraulicParts.AsReadOnly();

    /// <summary>
    /// List of injection parts
    /// </summary>
    public List<InjectionMeldingMachine> _injectionParts { get; } = new();
    public IReadOnlyCollection<InjectionMeldingMachine> InjectionParts => _injectionParts.AsReadOnly();

    /// <summary>
    /// List of robotic parts
    /// </summary>
    private List<RoboticAssemblyLine> _roboticParts { get; } = new();
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