using OpsForge.Application.DTOs;
using OpsForge.Application.Interfaces.Repositories;
using OpsForge.Application.Utilities;
using OpsForge.Domain.Entities.AggregateMachine.Inventory;

namespace OpsForge.Application.Services;

public sealed class MachineReadService
{
    private readonly IMachineRepository _repository;
    private readonly MachineUtility _utility;

    public MachineReadService(IMachineRepository repository, MachineUtility utility)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _utility = utility ?? throw new ArgumentNullException(nameof(utility));
    }

    public async Task<PartsSnapshot?> GetPartsSnapshotAsync(string machineName, CancellationToken cancellationToken = default)
    {
        Inventory? inventory = await _repository.GetInventoryByMachineNameAsync(machineName, cancellationToken);

        if (inventory is null)
            return null;

        return _utility.GetParts(inventory);
    }
}
