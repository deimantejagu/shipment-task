using DiscountsCalculator.Configs;
using DiscountsCalculator.Models;
using DiscountsCalculator.Services;

namespace DiscountsCalculator.Rules;

// All S shipments should always match the lowest S package price among the providers.
public class MatchSmallestSizePrices(FinancialTransaction transaction)
{
    private decimal _minPrice = FindLowestProvidersPrice();

    public decimal CalculateDiscount()
    {
        decimal providersPrice = 0;

        if(Check())
        {
            transaction.Price = _minPrice;

            foreach (var provider in ProvidersData.Providers)
            {
                if((provider.Provider == transaction.Provider) && (provider.Size == transaction.Size))
                {
                    providersPrice = provider.Price;
                }
            }

            transaction.Discount = providersPrice - _minPrice;
        }

        return transaction.Discount;
    }

    private bool Check()
    {
        return transaction.Size == "S";
    }

    private static decimal FindLowestProvidersPrice(){
        decimal minPrice = decimal.MaxValue;

        foreach (var provider in ProvidersData.Providers)
        {
            if((provider.Size == "S") && (minPrice > provider.Price))
            {
                minPrice = provider.Price;
            }
        }

        return minPrice;
    }
}