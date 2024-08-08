namespace DiscountsCalculator.Rules;

using DiscountsCalculator.Models;

public class MonthlyDiscountLimit(FinancialTransaction transaction, decimal monthlyDiscountSum)
{
    private const int MonthLimit = 10;

    public decimal CalculateDiscount()
    {
        if (Check())
        {
            transaction.Discount = transaction.Discount - monthlyDiscountSum + MonthLimit;

            return transaction.Discount;
        }

        return 0;
    }

    private bool Check()
    {
        return monthlyDiscountSum > MonthLimit;
    }
}