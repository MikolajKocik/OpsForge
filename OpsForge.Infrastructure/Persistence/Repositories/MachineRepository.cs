using Microsoft.EntityFrameworkCore;
using OpsForge.Application.Interfaces.Repositories;
using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine;
using OpsForge.Domain.Entities.AggregateMachine.Inventory;
using OpsForge.Domain.SeedWork.Interfaces;
using OpsForge.Infrastructure.Persistence.Contexts;
using OpsForge.Infrastructure.Utilities;

namespace OpsForge.Infrastructure.Persistence.Repositories;

internal sealed class  MachineRepository : IMachineRepository
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

    private IQueryable<T> GetQueryWithSpecification<T>(ISpecification<T> spec)
        where T : class, IAggregateRoot
    {
        IQueryable<T> queryable = this._context.Set<T>().AsQueryable();

        return ContextUtility.ApplySpecification(queryable, spec);
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

    public async Task<Machine?> GetBySpecAsync(ISpecification<Machine> spec)
    {
        var finalQuery = GetQueryWithSpecification(spec);
        return await finalQuery.FirstOrDefaultAsync();
    }

    public async Task<Inventory?> GetInventoryByMachineNameAsync(
        string machineName, CancellationToken cancellationToken = default)
    {
        var spec = new MachineWithInventoryPartsSpecification(machineName);
        var machine = await GetBySpecAsync(spec);

        return machine?.Inventory;
    }
}
