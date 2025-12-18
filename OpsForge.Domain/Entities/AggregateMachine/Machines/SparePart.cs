namespace OpsForge.Domain.Entities.AggregateMachine.Machines;

public record SparePart
{
    public string Brand { get; init; }
    public string Model { get; init; }
    public string? SerialNumber { get; init; }

    public SparePart(string brand, string model, string? serialNumber = null)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand cannot be empty");
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model cannot be empty");

        Brand = brand;
        Model = model;

        SerialNumber = string.IsNullOrEmpty(serialNumber) ? null : serialNumber;
    }
}
