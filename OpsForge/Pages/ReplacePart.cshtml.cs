using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpsForge.Application.CQRS.Commands.ReplaceSparePart;
using OpsForge.Application.Interfaces.Repositories;
using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine.Machines;

namespace OpsForge.Web.Pages
{
    public class ReplacePartModel : PageModel
    {
        private readonly IMediator mediator;
        private readonly IMachineRepository repository;

        public ReplacePartModel(IMediator mediator, IMachineRepository repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        [BindProperty]
        public int MachineId { get; set; }

        [BindProperty]
        public string SelectedPartType { get; set; }

        [BindProperty] 
        public SparePart NewPart { get; set; }

        [BindProperty]
        public List<string> AvailablePartTypes { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Machine? machine = await repository.GetMachineById(id, default);
            this.MachineId = id;

            if (machine is not null)
            {
                AvailablePartTypes = machine.GetType()
                  .GetProperties()
                  .Where(p => p.PropertyType == typeof(SparePart))
                  .Select(p => p.Name)
                  .ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await mediator.Send(new ReplacePartCommand(
                    this.MachineId,
                    this.SelectedPartType,
                    this.NewPart));
        
            if (result.IsFailure)
            {
                ModelState.AddModelError("", result.Error);
                return await OnGetAsync(this.MachineId);
            }

            TempData["Success"] = "The part has been replaced successful!";
            return RedirectToPage("./Details", new { id = this.MachineId });
        }
    }
}
