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

    public async Task<Machine?> GetMachineById(int id, CancellationToken cancellationToken)
    {

        Machine? machine = await this._context.Machines.FindAsync(id, cancellationToken);
        return machine;
    }
        
  
    /// <summary>
    /// Updates the specified shadow property of a machine entity to the current UTC date and time, and saves the change
    /// to the database.
    /// </summary>
    /// <remarks>This method sets the value of the specified shadow property to <see cref="DateTime.UtcNow"/>
    /// and persists the change asynchronously. The shadow property must be configured in the entity model; otherwise,
    /// an exception may be thrown.</remarks>
    /// <param name="machine">The <see cref="Machine"/> entity whose shadow property will be updated. Must not be <c>null</c>.</param>
    /// <param name="shadowPropName">The name of the shadow property to update. Must correspond to a valid property defined in the entity model.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns></returns>
    public async Task UpdateDateOfReplaceSparePart(Machine machine, string shadowPropName,
        CancellationToken cancellationToken)
    {
        this._context.Entry(machine).Property(shadowPropName).CurrentValue = DateTime.UtcNow;
        await this._context.SaveChangesAsync(cancellationToken);
    }
}
