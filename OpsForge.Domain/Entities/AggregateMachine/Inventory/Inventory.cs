using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.SeedWork;
using System.Collections.Generic;

namespace OpsForge.Domain.Entities.AggregateMachine.Inventory;

/// <summary>
/// Aggregate inventory stores the whole production parts in warehouse
/// </summary>
public sealed class Inventory : ValueObject
{
    private readonly List<SparePart> _parts = new();
    public IReadOnlyCollection<SparePart> Parts => this._parts.AsReadOnly();

    public void RemovePart(params SparePart[] parts)
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