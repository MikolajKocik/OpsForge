using OpsForge.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace OpsForge.Domain.Entities.AggregateMaintenance;

public sealed class MaintenanceSchedule : ValueObject
{
    public TimeSpan MaintenanceInterval { get; }
    public DateTime LastMaintenanceDate { get; }
    public MaintenanceType Type { get; }
    public string Notes { get; }

    public MaintenanceSchedule(
       DateTime lastMaintenanceDate,
       TimeSpan maintenanceInterval,
       MaintenanceType type,
       string notes)
    {
        if (maintenanceInterval <= TimeSpan.Zero)
            throw new ArgumentException("Maintenance interval must be positive");

        if (string.IsNullOrWhiteSpace(notes))
            throw new ArgumentException("Maintenance notes cannot be empty");

        LastMaintenanceDate = lastMaintenanceDate;
        MaintenanceInterval = maintenanceInterval;
        Type = type;
        Notes = notes;
    }

    public DateTime NextMaintenanceDate => 
        LastMaintenanceDate + MaintenanceInterval;    

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return LastMaintenanceDate;
        yield return MaintenanceInterval;
        yield return Type;
        yield return Notes;
    }
}

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
