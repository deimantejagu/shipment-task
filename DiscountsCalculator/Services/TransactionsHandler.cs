namespace DiscountsCalculator.Services;

using DiscountsCalculator.Models;
using DiscountsCalculator.Rules;
using System.Globalization;

public class TransactionsHandler(List<string> transactions)
{
    private const int FreeShipping = 3;

    public void Handle()
    {
        int transactionsCounter = 0;
        decimal monthlyDiscountSum = 0;
        decimal monthlyLimitReached = 0;
        DateTime lastFreeShipmenDate = DateTime.MinValue;
        bool freeShipmentAppliedThisMonth = false;

        foreach (string transactionString in transactions)
        {
            ValidateData validateData = new(transactionString);
            FinancialTransaction? transaction = validateData.Validate();

            if (transaction != null){
                transaction.Price = PriceFinder.Find(transaction);

                DateTime createdAt = ConvertStringIntoDateTime(transaction);

                MatchSmallestSizePrices MatchSmallestSizePrices = new(transaction);
                MatchSmallestSizePrices.CalculateDiscount();

                if ((transaction.Provider == "LP") && (transaction.Size == "L") && !freeShipmentAppliedThisMonth)
                {
                    transactionsCounter++;
                }

                ThirdFreeShipment ThirdFreeShipment = new(transaction, transactionsCounter);
                ThirdFreeShipment.CalculateDiscount();

                if (transactionsCounter == FreeShipping)
                {
                    transactionsCounter = 0;
                    freeShipmentAppliedThisMonth = true;
                }

                if (createdAt.Month != lastFreeShipmenDate.Month)
                {
                    monthlyDiscountSum = 0;
                    lastFreeShipmenDate = createdAt;
                    freeShipmentAppliedThisMonth = false;
                }

                monthlyDiscountSum += transaction.Discount;

                if (monthlyLimitReached == 0)
                {
                    MonthlyDiscountLimit monthlyDiscountLimit = new(transaction, monthlyDiscountSum);
                    monthlyLimitReached =  monthlyDiscountLimit.CalculateDiscount();
                }

                Console.WriteLine($"{transaction.CreatedAt:yyyy-MM-dd} {transaction.Size} {transaction.Provider} {transaction.Price:0.00} {(transaction.Discount == 0 ? "-" : transaction.Discount.ToString("0.00"))}");
            } 
            else
            {
                Console.WriteLine($"{transactionString} ignored");
            }
        }
    }

    private DateTime ConvertStringIntoDateTime(FinancialTransaction transaction)
    {
        try 
        {
            return DateTime.ParseExact(transaction.CreatedAt, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        } 
        catch 
        {
            throw;
        }
    }
}
