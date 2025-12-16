using OpsForge.Domain.SeedWork;

namespace OpsForge.Domain.Entities.Base;

/// <summary>
/// Provides a base 'POCO' class for entities that require audit tracking of creation and modification metadata.
/// </summary>
/// <remarks><para> <see cref="BaseEntity"/> includes properties for storing the creation and last modification
/// timestamps, as well as the user responsible for each action. </para> <para> Derived classes can use these properties
/// to implement consistent audit logging across the application. </para></remarks>
public abstract class BaseEntity : Entity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; }

    public void UpdateAudit(string user)
    {
        ModifiedAt = DateTime.UtcNow;
        ModifiedBy = user;

        if (CreatedAt == default)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = user;
        }
    }
}
