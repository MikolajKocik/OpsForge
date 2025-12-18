using OpsForge.Domain.SeedWork;

namespace OpsForge.Domain.Enums;

public sealed class Line : Enumeration
{
    public static Line M1 = new(1, nameof(M1));
    public static Line M2 = new(2, nameof(M2));
    public static Line M3 = new(3, nameof(M3));
    public static Line M4 = new(4, nameof(M4));
    public static Line M5 = new(5, nameof(M5));

    public Line(int id, string name)
        : base(id, name) { }
}
