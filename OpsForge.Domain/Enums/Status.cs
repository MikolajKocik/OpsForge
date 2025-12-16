using OpsForge.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace OpsForge.Domain.Enums;


public sealed class Status : Enumeration
{
    public static Status Open = new(1, nameof(Open));

    [Display(Name = "In progress")]
    public static Status InProgress = new(2, nameof(InProgress));
    public static Status Completed = new(3, nameof(Completed));
    public static Status Cancelled = new(4, nameof(Cancelled));

    public Status(int id, string name)
        : base(id, name) { }
}