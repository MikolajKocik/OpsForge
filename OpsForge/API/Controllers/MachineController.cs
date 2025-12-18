using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpsForge.Application.CQRS.Commands.ReplaceSparePart;
using OpsForge.Domain.DTOs;
using OpsForge.Domain.SeedWork;

namespace OpsForge.Web.API.Controllers;

[ApiController]
[Route("api/machines")]
public class MachineController : ControllerBase
{
    private readonly IMediator mediator;

    public MachineController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("{id}/replace-part")]   
    public async Task<IActionResult> ReplaceMachineSparePart(
        [FromRoute] int id,
        [FromBody] ReplacePartRequest request,
        CancellationToken cancellationToken
        )
    {
        Result result = await mediator.Send(new ReplacePartCommand(
            id, 
            request.PartType, 
            request.NewPart), cancellationToken);

        return result.IsFailure
            ? BadRequest(result.Error)
            : Ok(result);
    }
}
