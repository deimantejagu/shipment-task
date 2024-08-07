namespace DiscountsCalculator.Services;

using DiscountsCalculator.Configs;
using DiscountsCalculator.Models;

public class InitialTransactionPrice()
{
    public static void SetTransactionPrice(FinancialTransaction transaction){
        foreach (var provider in ProvidersData.Providers)
        {
            if((provider.Provider == transaction.Provider) && (provider.Size == transaction.Size))
            {
                transaction.Price = provider.Price;
            }
        }
    }
}