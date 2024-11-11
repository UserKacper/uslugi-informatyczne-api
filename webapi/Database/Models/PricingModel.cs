public record PricingModel
{
    public required Guid Id { get; set; }
    public required string Type { get; set; }
    public required string FullName { get; init; }
    public required string Plan { get; init; }
    public required double PricingMin { get; init; }
    public required string PricingCurrency { get; init; }
    public required bool FullyCustomizable { get; init; }
    public required string HelpWithIntegration { get; init; }
    public required string Documentation { get; init; }
    public required string EmergencySupport { get; init; }
    public string? Description { get; init; }
    public string[]? KeyFeatures { get; init; }
    public int? ResponseTimeInHours { get; init; }
    public bool IsAvailable24x7 { get; init; }
}