using DiscountsCalculator.Configs;
using DiscountsCalculator.Models;

namespace DiscountsCalculator.Rules;

public class MatchSmallestSizePrices()
{
    public FinancialTransaction Apply(FinancialTransaction transaction)
    {
        if (Check(transaction))
        {
            decimal minPrice = FindLowestProvidersPrice();

            transaction.Discount = transaction.Price - minPrice;
            transaction.Price = minPrice;
        }

        return transaction;
    }

    private bool Check(FinancialTransaction transaction)
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