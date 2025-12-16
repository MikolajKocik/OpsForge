using OpsForge.Domain.SeedWork;

namespace OpsForge.Domain.Entities.AggregateMachine;

public sealed class MachineSpecification : ValueObject
{
    public string Model { get; }
    public string Manufacturer { get; }
    public double PowerKw { get; }    
    public double Voltage { get; }       
    public double WeightKg { get; }       
    public Dimensions Dimensions { get; }  
    public string Material { get; }       
    public string Description { get; }

    public MachineSpecification(
        string model,
        string manufacturer,
        double powerKw,
        double voltage,
        double weightKg,
        Dimensions dimensions,
        string material,
        string description
        )
    {
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model is required");

        if (string.IsNullOrWhiteSpace(manufacturer))
            throw new ArgumentException("Manufacturer is required");

        if (powerKw <= 0)
            throw new ArgumentException("Power must be positive");

        if (voltage <= 0)
            throw new ArgumentException("Voltage must be positive");

        if (weightKg <= 0)
            throw new ArgumentException("Weight must be positive");

        if (dimensions.Length <= 0 || dimensions.Width <= 0 || dimensions.Height <= 0)
            throw new ArgumentException("Dimensions must be positive");


        Model = model;
        Manufacturer = manufacturer;
        PowerKw = powerKw;
        Voltage = voltage;
        WeightKg = weightKg;
        Dimensions = dimensions;
        Material = material;
        Description = description;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Model;
        yield return Manufacturer;
        yield return PowerKw;
        yield return Voltage;
        yield return WeightKg;
        yield return Dimensions;
        yield return Material;
    }
}

