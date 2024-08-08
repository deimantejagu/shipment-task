namespace DiscountsCalculator.Services;

using DiscountsCalculator.Configs;
using DiscountsCalculator.Models;
using DiscountsCalculator.Rules;

public class TransactionsHandler(List<FinancialTransaction> transactions)
{
    public void Handle()
    {
        int counter = 0;
        decimal monthlyDiscountSum = 0;
        decimal monthlyLimitReached = 0;
        DateTime lastFreeShipmenDate = DateTime.MinValue;
        bool freeShipmentAppliedThisMonth = false;

        SetTransactionPrice(transactions);

        foreach (var transaction in transactions)
        {
            MatchSmallestSizePrices MatchSmallestSizePrices = new(transaction);
            MatchSmallestSizePrices.CalculateDiscount();

            if((transaction.Provider == "LP") && (transaction.Size == "L") && !freeShipmentAppliedThisMonth)
            {
                counter++;
            }

            ThirdFreeShipment ThirdFreeShipment = new(transaction, counter);
            ThirdFreeShipment.CalculateDiscount();

            if(counter == 3)
            {
                counter = 0;
                freeShipmentAppliedThisMonth = true;
            }

            if(transaction.CreatedAt.Month != lastFreeShipmenDate.Month)
            {
                // Console.WriteLine($"Monthly Discount Sum: {monthlyDiscountSum:0.00}");
                monthlyDiscountSum = 0;
                lastFreeShipmenDate = transaction.CreatedAt;
                freeShipmentAppliedThisMonth = false;
            }

            monthlyDiscountSum += transaction.Discount;

            if(monthlyLimitReached == 0)
            {
                MonthlyDiscountLimit monthlyDiscountLimit = new(transaction, monthlyDiscountSum);
                monthlyLimitReached =  monthlyDiscountLimit.CalculateDiscount();
            }

            Console.WriteLine($"{transaction.CreatedAt:yyyy-MM-dd} {transaction.Size} {transaction.Provider} {transaction.Price:0.00} {(transaction.Discount == 0 ? "-" : transaction.Discount.ToString("0.00"))}");
        }

        // if (monthlyDiscountSum > 0)
        // {
        //     Console.WriteLine($"Monthly Discount Sum: {monthlyDiscountSum:0.00}");
        // }
    }

    private static void SetTransactionPrice(List<FinancialTransaction> transactions){
        foreach (var transaction in transactions)
        {
            foreach(var provider in ProvidersData.Providers)
            {
                if((provider.Provider == transaction.Provider) && (provider.Size == transaction.Size))
                {
                    transaction.Price = provider.Price;
                }
            }
        }
    }
}
