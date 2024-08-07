namespace DiscountsCalculator.Rules;

using DiscountsCalculator.Configs;
using DiscountsCalculator.Models;

// The third L shipment via LP should be free, but only once a calendar month.
public class ThirdFreeShipment(FinancialTransaction transaction, int counter)
{
    public decimal CalculateDiscount()
    {
        decimal providersPrice = 0;

        foreach (var provider in ProvidersData.Providers)
        {
            if((provider.Provider == transaction.Provider) && (provider.Size == transaction.Size))
            {
                providersPrice = provider.Price;
            }
        }

        if(Check())
        {
            transaction.Price = 0;
            transaction.Discount = providersPrice;
        }
        else if((transaction.Provider == "LP") && (transaction.Size == "L"))
        {
            transaction.Price = providersPrice;
        }

        return transaction.Discount;
    }

    private bool Check()
    {
        return counter == 3;
    }
}