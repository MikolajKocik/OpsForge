using MediatR;
using Microsoft.Extensions.Logging;
using OpsForge.Application.Interfaces.Repositories;
using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.SeedWork;
using System.Reflection;

namespace OpsForge.Application.CQRS.Commands.ReplaceSparePart;

public sealed class ReplacePartCommandHandler : IRequestHandler<ReplacePartCommand, Result>
{
    private readonly IMachineRepository machineRepository;
    private readonly ILogger<ReplacePartCommandHandler> logger;

    public ReplacePartCommandHandler(IMachineRepository machineRepository,
        ILogger<ReplacePartCommandHandler> logger)
    {
        this.machineRepository = machineRepository;
        this.logger = logger;
    }

    public async Task<Result> Handle(ReplacePartCommand request, CancellationToken cancellationToken)
    {
        if (request.PartType is null || request.NewPart is null)
        {
            logger.LogWarning("Warning: Provided spare part is null");
            return Result.Failure("The spare part to change is null");
        }

        logger.LogInformation("Request: Get machine by machine's id");
        Machine? machine = await this.machineRepository.GetMachineById(request.MachineId, cancellationToken);

        if (machine is null)
        {
            logger.LogWarning("Warning: Machine with id:{id} not found", request.MachineId);
            return Result.Failure("Machine not found");
        }

        PropertyInfo? property = machine.GetType().GetProperty(request.PartType);
        if (property is null || property.PropertyType != typeof(SparePart))
        {
            return Result.Failure($"Machine has not part with name {request.PartType}");
        }

        property.SetValue(machine, request.NewPart);

        string shadowPropName = $"{request.PartType}_LastReplaced";
        await this.machineRepository.UpdateDateOfReplaceSparePart(machine, shadowPropName, cancellationToken);

        return Result.Success();
    }
}
