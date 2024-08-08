namespace DiscountsCalculator.Rules;

using DiscountsCalculator.Models;
using DiscountsCalculator.Services;

public class ThirdFreeShipment(FinancialTransaction transaction, int counter)
{
    public decimal CalculateDiscount()
    {
        decimal providersPrice = PriceFinder.Find(transaction);

        if (Check())
        {
            transaction.Price = 0;
            transaction.Discount = providersPrice;
        }
        else if ((transaction.Provider == "LP") && (transaction.Size == "L"))
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