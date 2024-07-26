public record SmartHomeInformatioModel
{
    public required Guid Id { get; set; }
    public required string FullName { get; init; }
    public required double PricingMin { get; init; }
    public required string PricingCurrency { get; init; }
    public required string PanelAdminDesc { get; init; }
    public required string MobileAccessControl { get; init; }
    public required string HelpWithIntegration { get; init; }
    public required string Documentation { get; init; }
    public required string Emergency { get; init; }
}