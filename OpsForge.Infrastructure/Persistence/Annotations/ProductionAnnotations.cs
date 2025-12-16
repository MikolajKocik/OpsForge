namespace OpsForge.Infrastructure.Persistence.Annotations;

internal  static class ProductionAnnotations
{
    // Documentation
    public const string Description = "Documentation:Description";
    public const string OwnerTeam = "Documentation:OwnerTeam";

    // Monitoring & Deployment
    public const string HighAvailability = "Monitoring:HA_Required";
    public const string SchemaPrefix = "Deployment:SchemaPrefix";
    public const string ContextCriticality = "Deployment:Criticality";
}
