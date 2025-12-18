using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine.Inventory;
using OpsForge.Domain.SeedWork.Interfaces;

namespace OpsForge.Application.Interfaces.Repositories;

public interface IMachineRepository : IRepository<Machine>
{
    Task<Inventory?> GetInventoryByMachineNameAsync(string machineName, CancellationToken cancellationToken = default);
    Task<Machine?> GetBySpecAsync(ISpecification<Machine> spec);
    Task<Machine?> GetMachineById(int id, CancellationToken cancellationToken);
    Task UpdateDateOfReplaceSparePart(Machine machine, string shadowPropName, CancellationToken cancellationToken);
}
