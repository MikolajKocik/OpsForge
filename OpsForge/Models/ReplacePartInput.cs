using System.ComponentModel.DataAnnotations;

namespace OpsForge.Web.Models;

public sealed class ReplacePartInput
{
    [Required(ErrorMessage = "Brand is required")]
    public string Brand { get; set; } = string.Empty;

    [Required(ErrorMessage = "Model is required")]
    public string Model { get; set; } = string.Empty;
    public string? SerialNumber { get; set; }
}
