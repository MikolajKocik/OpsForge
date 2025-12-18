using OpsForge.Domain.SeedWork;

namespace OpsForge.Domain.Entities.AggregateMachine;

public readonly struct Dimensions 
{
    public double Length { get; }
    public double Width { get; }
    public double Height { get; }

    public Dimensions(
        double length,
        double width,
        double height
        )
    {
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }
}
