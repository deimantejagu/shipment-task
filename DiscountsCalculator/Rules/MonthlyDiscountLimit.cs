namespace DiscountsCalculator.Rules;

using DiscountsCalculator.Configs;
using DiscountsCalculator.Models;

// Accumulated discounts cannot exceed 10 € in a calendar month. If there are not enough funds to fully 
// cover a discount this calendar month, it should be covered partially.

// Patikrinti, kiek pinigu yra sunaudota nuolaidoms
// Ar daugiau uz 10?
// Jeigu pinigu neuztenka padengti dalinai
// Jei yra virsijamas limitas kitoms prekems nuolaidos netaikomos
public class MonthlyDiscountLimit(FinancialTransaction transaction, decimal monthlyDiscountSum)
{
    public decimal CalculateDiscount()
    {
        if(Check())
        {
            // Console.WriteLine($"Dalinai dengia: {transaction.Discount - monthlyDiscountSum + 10}");
            transaction.Discount = transaction.Discount - monthlyDiscountSum + 10;

            return transaction.Discount;
        }
        // else 
        // {
        //     Console.WriteLine("Limitas nepasiektas");
        // }

        return 0;
    }

    private bool Check()
    {
        return monthlyDiscountSum > 10;
    }
}