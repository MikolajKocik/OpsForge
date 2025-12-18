using MediatR;
using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.SeedWork;

namespace OpsForge.Application.CQRS.Commands.ReplaceSparePart;

public record ReplacePartCommand(
    int MachineId,
    string PartType,
    SparePart NewPart
    ) : IRequest<Result>;
