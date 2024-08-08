using DiscountsCalculator.Configs;
using DiscountsCalculator.Models;
using DiscountsCalculator.Services;

namespace DiscountsCalculator.Rules;

public class MatchSmallestSizePrices(FinancialTransaction transaction)
{
    private decimal _minPrice = FindLowestProvidersPrice();

    public decimal CalculateDiscount()
    {
        if (Check())
        {
            transaction.Price = _minPrice;
            transaction.Discount = PriceFinder.Find(transaction) - _minPrice;
        }

        return transaction.Discount;
    }

    private bool Check()
    {
        return transaction.Size == "S";
    }

    private static decimal FindLowestProvidersPrice()
    {
        decimal minPrice = decimal.MaxValue;

        foreach (ProviderInformation provider in ProvidersData.Providers)
        {
            if ((provider.Size == "S") && (minPrice > provider.Price))
            {
                minPrice = provider.Price;
            }
        }

        return minPrice;
    }
}