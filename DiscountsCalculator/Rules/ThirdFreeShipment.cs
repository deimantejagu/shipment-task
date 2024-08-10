namespace DiscountsCalculator.Rules;

using DiscountsCalculator.Models;
using DiscountsCalculator.Services;

public class ThirdFreeShipment()
{
    private const int FreeTransactionAfterCount = 3;

    public FinancialTransaction Apply(FinancialTransaction transaction, List<FinancialTransaction> completedTransactions)
    {
        if (Check(transaction, completedTransactions))
        {
            transaction.Discount = transaction.Price;
            transaction.Price = 0;
        }

        return transaction;
    }

    private bool Check(FinancialTransaction transaction, List<FinancialTransaction> completedTransactions)
    {
        if (transaction.Provider != "LP" || transaction.Size != "L")
        {
            return false;
        }

        DateTime createdAt = StringIntoDateTimeConverter.Convert(transaction);

        List<FinancialTransaction> previousTransactions = completedTransactions
            .Where(t => t.Provider == "LP" && t.Size == "L" &&
                StringIntoDateTimeConverter.Convert(t).Year == createdAt.Year &&
                StringIntoDateTimeConverter.Convert(t).Month == createdAt.Month &&
                StringIntoDateTimeConverter.Convert(t) < createdAt)
            .ToList();

        return previousTransactions.Count == FreeTransactionAfterCount - 1;
    }
}