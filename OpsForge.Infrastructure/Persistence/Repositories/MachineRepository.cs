using Microsoft.EntityFrameworkCore;
using OpsForge.Application.Interfaces.Repositories;
using OpsForge.Domain.Entities;
using OpsForge.Domain.SeedWork.Interfaces;
using OpsForge.Infrastructure.Persistence.Contexts;

namespace OpsForge.Infrastructure.Persistence.Repositories;

internal class MachineRepository : IMachineRepository
{
    private readonly MachineContext _context;

    public IUnitOfWork UnitOfWork
    {
        get
        {
            return this._context;
        }
    }

    public MachineRepository(MachineContext context)
    {
        this._context = context 
            ?? throw new ArgumentNullException(nameof(context));
    }

    // specification => IQueryable
    private IQueryable<T> ApplySpecification<T>(IQueryable<T> query, ISpecification<T> spec)
        where T : class
    {
        // includes relations (expressions)
        IQueryable<T> queryableWithIncludes = spec.Includes
            .Aggregate(query,
                (current, include) => current.Include(include));

        // includes relations (nested)
        IQueryable<T> secondaryResult = spec.IncludeStrings
            .Aggregate(queryableWithIncludes,
                (current, include) => current.Include(include));

        return secondaryResult.Where(spec.Criteria);
    }

    private IQueryable<T> GetQueryWithSpecification<T>(ISpecification<T> spec)
        where T : class, IAggregateRoot
    {
        IQueryable<T> queryable = this._context.Set<T>().AsQueryable();

        return ApplySpecification(queryable, spec);
    }

    public void Add(Machine entity)
        => this._context.Set<Machine>().Add(entity);

    public void Update(Machine entity)
        => this._context.Set<Machine>().Update(entity);

    public void Delete(Machine entity)
        => this._context.Set<Machine>().Remove(entity);

    public async Task<List<Machine>> ListAsync(ISpecification<Machine> spec)
    {
        var finalQuery = GetQueryWithSpecification(spec);
        return await finalQuery.ToListAsync();
    }

    public async Task<Machine?> GetSingleBySpecAsync(ISpecification<Machine> spec)
    {
        var finalQuery = GetQueryWithSpecification(spec);
        return await finalQuery.SingleOrDefaultAsync();
    }
}
