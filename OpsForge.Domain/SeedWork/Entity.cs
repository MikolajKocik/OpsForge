using OpsForge.Domain.SeedWork.Interfaces;

namespace OpsForge.Domain.SeedWork;

public abstract class Entity
{
    private int? _requestHashCode;
    private int _id;
    private List<INotification> _domainEvents;

    protected Entity()
    {
        _domainEvents = new List<INotification>();
    }

    public virtual int Id
    {
        get
        {
            return this._id;
        }
        protected set
        {
            this._id = value;
        }
    }

    public List<INotification> DomainEvents
        => this._domainEvents;

    /// <summary>
    /// Adds a domain event to the collection of events associated with the entity.
    /// </summary>
    /// <remarks>Domain events are used to signal that something of significance has occurred within the
    /// entity. The event will be available for processing until the collection of domain events is cleared or
    /// dispatched.</remarks>
    /// <param name="eventItem">The domain event to add. Cannot be <c>null</c>.</param>
    public void AddDomainEvent(INotification eventItem)
    {
        this._domainEvents.Add(eventItem);
    }

    /// <summary>
    /// Removes the specified domain event from the collection of domain events.
    /// </summary>
    /// <remarks>If the specified event is not present in the collection, no action is taken.</remarks>
    /// <param name="eventItem">The domain event to remove from the collection. Cannot be <c>null</c>.</param>
    public void RemoveDomainEvent(INotification eventItem)
    {
        this._domainEvents.Remove(eventItem);
    }

    /// <summary>
    /// Determines whether the current entity is transient, meaning it has not been assigned a persistent identifier.
    /// </summary>
    /// <remarks>A transient entity is typically one that has not yet been saved to a data store and therefore
    /// does not have a persistent identifier assigned.</remarks>
    /// <returns><see langword="true"/> if the entity's <c>Id</c> property is equal to its default value; otherwise, <see
    /// langword="false"/>.</returns>
    public bool IsTransient() => this.Id == default(Int32);

    /// <summary>
    /// Returns a hash code for the current object based on its identifier.
    /// </summary>
    /// <remarks>For non-transient instances, the hash code is derived from the object's identifier and is
    /// cached for performance.  For transient instances, the base implementation is used. This behavior ensures
    /// consistent hash codes for persisted entities.</remarks>
    /// <returns>An integer hash code representing the current object.</returns>
    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!this._requestHashCode.HasValue)
                this._requestHashCode = this.Id.GetHashCode() ^ 31;

            return this._requestHashCode.Value;
        }
        else
            return base.GetHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="Entity"/> instances are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Entity"/> to compare.</param>
    /// <param name="right">The second <see cref="Entity"/> to compare.</param>
    /// <returns><see langword="true"/> if the specified <see cref="Entity"/> instances are equal; otherwise, <see
    /// langword="false"/>.</returns>
    public static bool operator ==(Entity left, Entity right)
    {
        if (Object.Equals(left, null))
            return (Object.ReferenceEquals(right, null));
        else
            return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="Entity"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Entity"/> to compare.</param>
    /// <param name="right">The second <see cref="Entity"/> to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> do not represent the same value;
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}
