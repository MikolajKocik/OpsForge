using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpsForge.Application.CQRS.Commands.ReplaceSparePart;
using OpsForge.Application.Interfaces.Repositories;
using OpsForge.Domain.Entities;
using OpsForge.Domain.Entities.AggregateMachine.Machines;
using System.ComponentModel.DataAnnotations;

namespace OpsForge.Web.Pages;

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
    [Required(ErrorMessage = "Please select a part from the warehouse for assembly")]
    public string SelectedInventoryPartId { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Choose the spare part to change")]
    public string SelectedPartType { get; set; }

    public List<SelectListItem> AvailablePartTypes { get; set; } = new();
    public List<SelectListItem> InventoryPartOptions { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id == 0)
        {
            return RedirectToPage("/Index");
        }

        var machine = await repository.GetMachineById(id, default); 
        if (machine is null) return NotFound();

        this.MachineId = id;
        await LoadPageData(machine);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Machine? machine = await this.repository.GetMachineById(this.MachineId, default);
        if (machine is null) return NotFound();

        await LoadPageData(machine);

        var isPartTypeValid = this.AvailablePartTypes.Any(p => p.Value == this.SelectedPartType);
        if (!isPartTypeValid)
        {
            ModelState.AddModelError(nameof(SelectedPartType), "Invalid spare part");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        SparePart? partFromInventory = machine.Inventory.Parts
           .FirstOrDefault(p => p.SerialNumber == SelectedInventoryPartId);

        if (partFromInventory is null)
        {
            ModelState.AddModelError(nameof(SelectedInventoryPartId), "The selected spare part is invalid.");
            return Page();
        }

        var result = await this.mediator.Send(new ReplacePartCommand(
            this.MachineId,
            this.SelectedPartType,
            partFromInventory));

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error);
            return Page();
        }

        TempData["Success"] = "Czêœæ zosta³a pomyœlnie zamontowana.";
        return RedirectToPage("./Details", new { id = this.MachineId });
    }

    private async Task LoadPageData(Machine machine)
    {
        this.AvailablePartTypes = machine.GetType().GetProperties()
            .Where(p => p.PropertyType == typeof(SparePart))
            .Select(p => new SelectListItem { Text = p.Name, Value = p.Name })
            .ToList();

        this.InventoryPartOptions = machine.Inventory.Parts
            .Select(part => new SelectListItem
            {
                Text = $"{part.Brand} {part.Model} (SN: {part.SerialNumber ?? "N/A"})",
                Value = part.SerialNumber
            })
            .ToList();
    }
}
