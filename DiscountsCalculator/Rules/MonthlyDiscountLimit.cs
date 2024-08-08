namespace DiscountsCalculator.Rules;

using DiscountsCalculator.Models;

// Accumulated discounts cannot exceed 10 € in a calendar month. If there are not enough funds to fully 
// cover a discount this calendar month, it should be covered partially.
public class MonthlyDiscountLimit(FinancialTransaction transaction, decimal monthlyDiscountSum)
{
    public decimal CalculateDiscount()
    {
        if(Check())
        {
            transaction.Discount = transaction.Discount - monthlyDiscountSum + 10;

            return transaction.Discount;
        }

        return 0;
    }

    private bool Check()
    {
        return monthlyDiscountSum > 10;
    }
}