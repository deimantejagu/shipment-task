namespace DiscountsCalculator.Models;

public class ProviderInformation(string provider, string size, decimal price)
{
    public string Provider { get; set; } = provider;
    public string Size { get; set; } = size;
    public decimal Price { get; set; } = price;
}
