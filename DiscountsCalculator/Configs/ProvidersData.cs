namespace DiscountsCalculator.Configs;

using DiscountsCalculator.Models;

public class ProvidersData
{
    public static List<ProviderInformation> Providers { get; } =
    [
        new ProviderInformation("LP", "S", 1.50m),
        new ProviderInformation("LP", "M", 4.90m),
        new ProviderInformation("LP", "L", 6.90m),
        new ProviderInformation("MR", "S", 2m),
        new ProviderInformation("MR", "M", 3m),
        new ProviderInformation("MR", "L", 4m),
    ];
}
