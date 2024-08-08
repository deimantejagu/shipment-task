namespace DiscountsCalculator.Rules;

using DiscountsCalculator.Models;
using DiscountsCalculator.Services;

public class ThirdFreeShipment(FinancialTransaction transaction, int counter)
{
    public decimal CalculateDiscount()
    {
        if (Check())
        {
            transaction.Discount = transaction.Price;
            transaction.Price = 0;
        }
        else if ((transaction.Provider == "LP") && (transaction.Size == "L"))
        {
            transaction.Price = transaction.Price;
        }

        return transaction.Discount;
    }

    private bool Check()
    {
        return counter == 3;
    }
}