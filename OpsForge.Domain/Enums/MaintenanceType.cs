using OpsForge.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace OpsForge.Domain.Enums;

public sealed class MaintenanceType : Enumeration
{
    [Display(Name = "Full check")]
    public static MaintenanceType FullCheck => new(1, nameof(FullCheck));
    [Display(Name = "Oil change")]
    public static MaintenanceType OilChange => new(2, nameof(OilChange));
    public static MaintenanceType Inspection => new(3, nameof(Inspection));

    public MaintenanceType(int id, string name)
        : base(id, name) { }
}

