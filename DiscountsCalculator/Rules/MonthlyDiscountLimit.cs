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
    public void CalculateDiscount()
    {
        if(Check())
        {
            // Console.WriteLine($"Pasiektas limitas: {monthlyDiscountSum - 10}");
        }
    }

    private bool Check()
    {
        return monthlyDiscountSum > 10;
    }
}