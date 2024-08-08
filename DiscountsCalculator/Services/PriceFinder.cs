namespace DiscountsCalculator.Services;

using DiscountsCalculator.Models;
using DiscountsCalculator.Configs;

public class PriceFinder
{
    public static decimal Find(FinancialTransaction transaction){
        foreach (ProviderInformation provider in ProvidersData.Providers)
        {
            if ((provider.Provider == transaction.Provider) && (provider.Size == transaction.Size))
            {
                return provider.Price;
            }
        }

        return 0;
    }
}
