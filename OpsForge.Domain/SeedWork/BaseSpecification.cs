using OpsForge.Domain.SeedWork.Interfaces;
using System.Linq.Expressions;
namespace OpsForge.Domain.SeedWork;

/// <summary>
/// Provides a base implementation for defining query specifications, including filtering criteria and related entity
/// includes, for use with repositories or data access layers.
/// </summary>
/// <remarks><para> <see cref="BaseSpecification{T}"/> enables the encapsulation of query logic, such as filtering
/// conditions and navigation property includes, in a reusable and composable manner. Derived classes can specify
/// criteria and related entities to include when querying data sources. </para> <para> This class is typically used in
/// conjunction with repository patterns to separate query definitions from data access logic. </para></remarks>
/// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
public abstract class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        this.Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; }
        = new List<Expression<Func<T, object>>>();

    public List<string> IncludeStrings { get; } = new List<string>();

    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        this.Includes.Add(includeExpression);
    }

    // string-based includes allow for including children of children
    // for example, Basket.Items.Product
    protected virtual void AddInclude(string includeString)
    {
        this.IncludeStrings.Add(includeString);
    }
}
