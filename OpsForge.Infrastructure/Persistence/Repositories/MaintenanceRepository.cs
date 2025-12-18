using Microsoft.EntityFrameworkCore;
using OpsForge.Application.Interfaces.Repositories;
using OpsForge.Domain.Entities.AggregateMaintenance;
using OpsForge.Domain.SeedWork.Interfaces;
using OpsForge.Infrastructure.Persistence.Contexts;
using OpsForge.Infrastructure.Utilities;

namespace OpsForge.Infrastructure.Persistence.Repositories;

internal sealed class MaintenanceRepository : IMaintenanceRepository
{
    private readonly MaintenanceContext _context;

    public IUnitOfWork UnitOfWork
    {
        get
        {
            return this._context;
        }
    }

    public MaintenanceRepository(MaintenanceContext context)
    {
        _context = context
            ?? throw new ArgumentException(nameof(context));
    }

    private IQueryable<T> GetQueryWithSpecification<T>(ISpecification<T> spec)
     where T : class, IAggregateRoot
    {
        IQueryable<T> queryable = this._context.Set<T>().AsQueryable();

        return ContextUtility.ApplySpecification(queryable, spec);
    }

    public void Add(MaintenanceOrder entity)
        => this._context.Add(entity);
    public void Update(MaintenanceOrder entity)
        => this._context.Update(entity);

    public void Delete(MaintenanceOrder entity)
        => this._context.Remove(entity);

    public async Task<List<MaintenanceOrder>> ListAsync(ISpecification<MaintenanceOrder> spec)
    {
        var finalQuery = GetQueryWithSpecification(spec);
        return await finalQuery.ToListAsync();
    }
}
