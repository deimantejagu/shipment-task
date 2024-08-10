using DiscountsCalculator.Services;

namespace DiscountsCalculator.Models;

public class FinancialTransaction
{
    public string CreatedAt { get; private set; }
    public string Size { get; private set; }
    public string Provider { get; private set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }

    public FinancialTransaction(string createdAt, string size, string provider)
    {
        CreatedAt = createdAt;
        Size = size;
        Provider = provider;
        Price = PriceSetter.SetInitialPrice(this);
    }
}
