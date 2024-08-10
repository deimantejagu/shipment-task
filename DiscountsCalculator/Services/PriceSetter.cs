using DiscountsCalculator.Models;
using DiscountsCalculator.Configs;

namespace DiscountsCalculator.Services;

public class PriceSetter()
{
    public static decimal Set(FinancialTransaction transaction)
    {
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
