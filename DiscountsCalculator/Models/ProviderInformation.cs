namespace DiscountsCalculator.Models;

public class ProviderInformation(string provider, string size, decimal price)
{
    public string Provider { get; private set; } = provider;
    public string Size { get; private set; } = size;
    public decimal Price { get; private set; } = price;
}
