namespace DiscountsCalculator.Services;

using DiscountsCalculator.Models;
using DiscountsCalculator.Configs;

public class PriceSetter()
{
    public static decimal SetInitialPrice(FinancialTransaction transaction){
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
