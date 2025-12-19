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
    [Required(ErrorMessage = "Please, choose the part to replace")]
    public string SelectedInventoryPartId { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Wybierz podzespó³ do wymiany")]
    public string SelectedPartType { get; set; }

    public List<SelectListItem> AvailablePartTypes { get; set; } = new();
    public List<SelectListItem> InventoryPartOptions { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var machine = await repository.GetMachineById(id, default); 
        if (machine is null) return NotFound();

        this.MachineId = id;
        await LoadPageData(machine);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await ReloadData();
            return Page();
        }

        Machine? machine = await this.repository.GetMachineById(this.MachineId, default);
        if (machine is null) return NotFound();

        if (!int.TryParse(SelectedInventoryPartId, out int partIndex) ||
            partIndex >= machine.Inventory.Parts.Count)
        {
            ModelState.AddModelError("", "Invalid choice from inventory");
            await LoadPageData(machine);
            return Page();
        }

        SparePart? partFromInventory = machine?.Inventory.Parts.ElementAt(partIndex);
        if (partFromInventory is null) return NotFound();

        var result = await this.mediator.Send(new ReplacePartCommand(
            this.MachineId,
            this.SelectedPartType,
            partFromInventory));

        if (result.IsFailure)
        {
            ModelState.AddModelError("", result.Error);
            await LoadPageData(machine);
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
            .Select((part, index) => new SelectListItem
            {
                Text = $"{part.Brand} {part.Model} (SN: {part.SerialNumber ?? "N/A"})",
                Value = index.ToString()
            })
            .ToList();
    }

    private async Task ReloadData()
    {
        var machine = await repository.GetMachineById(this.MachineId, default);
        if (machine is not null) await LoadPageData(machine);
    }
}
