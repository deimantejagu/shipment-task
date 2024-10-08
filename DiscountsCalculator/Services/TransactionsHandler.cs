using DiscountsCalculator.Models;
using DiscountsCalculator.Rules;

namespace DiscountsCalculator.Services;

public class TransactionsHandler(List<string> transactions)
{
    public void Handle()
    {
        List<FinancialTransaction> completedTransactions = [];

        foreach (string transactionString in transactions)
        {
            DataValidator dataValidator = new(transactionString);
            FinancialTransaction? transaction = dataValidator.Validate();

            if (transaction != null)
            {
                MatchSmallestSizePrices matchSmallestSizePrices = new();
                ThirdFreeShipment thirdFreeShipment = new();
                MonthlyDiscountLimit monthlyDiscountLimit = new();

                transaction = matchSmallestSizePrices.Apply(transaction);
                transaction = thirdFreeShipment.Apply(transaction, completedTransactions);
                transaction = monthlyDiscountLimit.Apply(transaction, completedTransactions);

                completedTransactions.Add(transaction);

                Console.WriteLine($"{transaction.CreatedAt:yyyy-MM-dd}" +
                    $" {transaction.Size} {transaction.Provider}" +
                    $" {transaction.Price:0.00} {(transaction.Discount == 0 ? "-" : transaction.Discount.ToString("0.00"))}");
            }
            else
            {
                Console.WriteLine($"{transactionString} Ignored");
            }
        }
    }
}
