using OpsForge.Domain.Entities.AggregateMachine.Machines;

namespace OpsForge.Domain.DTOs;

public record ReplacePartRequest(string PartType, SparePart NewPart);
