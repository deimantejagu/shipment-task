namespace DiscountsCalculator.Models;

public class FinancialTransaction(string createdAt, string size, string provider)
{
    public string CreatedAt { get; private set; } = createdAt;
    public string Size { get; private set; } = size;
    public string Provider { get; private set; } = provider;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}
