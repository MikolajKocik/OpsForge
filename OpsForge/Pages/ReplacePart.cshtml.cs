using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpsForge.Application.CQRS.Commands.ReplaceSparePart;
using OpsForge.Application.Interfaces.Repositories;
using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine.Machines;
using OpsForge.Domain.SeedWork;
using OpsForge.Web.Models;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please, choose the part to replace")]
        public string SelectedPartType { get; set; }

        [BindProperty]
        public List<SelectListItem> PartTypeOptions { get; set; } = new(); 

        [BindProperty]
        public ReplacePartInput Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Machine? machine = await repository.GetMachineById(id, default);
            this.MachineId = id;

            if (machine is not null)
            {
                PartTypeOptions = machine.GetType()
                  .GetProperties()
                  .Where(p => p.PropertyType == typeof(SparePart))
                  .Select(p => new SelectListItem
                  {
                      Text = p.Name,
                      Value = p.Name
                  })
                  .ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var newPart = new SparePart(this.Input.Brand, this.Input.Model, this.Input.SerialNumber);

            Result? result = await mediator.Send(new ReplacePartCommand(
                    this.MachineId,
                    this.SelectedPartType,
                    newPart));

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
