using OpsForge.Domain.SeedWork;

namespace OpsForge.Domain.Entities.AggregateMachine;

public sealed class MachineSpecification : ValueObject
{
    private string _model;
    private string _manufacturer;
    private double _powerKw;
    private double _voltage;
    private double _weightKg;
    private string _material;
    private string _description;

    // shadow properties for dimensions
    private double _dimensions_Length;
    private double _dimensions_Width;
    private double _dimensions_Height;

    public string Model => this._model;
    public string Manufacturer => this._manufacturer;
    public double PowerKw => this._powerKw;
    public double Voltage => this._voltage;
    public double WeightKg => this._weightKg;
    public string Material => this._material;
    public string Description => this._description;
    public Dimensions Dimensions => 
        new Dimensions(
            this._dimensions_Length,
            this._dimensions_Width,
            this._dimensions_Height
        );


    private MachineSpecification() { }

    public MachineSpecification()
    {
        
    }

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


        this._model = model;
        this._manufacturer = manufacturer;
        this._powerKw = powerKw;
        this._voltage = voltage;
        this._weightKg = weightKg;
        this._material = material;
        this._description = description;


        this._dimensions_Length = dimensions.Length;
        this._dimensions_Width = dimensions.Width;
        this._dimensions_Height = dimensions.Height;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this._model;
        yield return this._manufacturer;
        yield return this._powerKw;
        yield return this._voltage;
        yield return this._weightKg;
        yield return this._material;
    }
}

