using DiscountsCalculator.Models;
using DiscountsCalculator.Services;

namespace DiscountsCalculator.Rules;

public class MonthlyDiscountLimit()
{
    private const int MonthLimit = 10;
    private decimal _monthlyDiscountSum = 0;

    public FinancialTransaction Apply(FinancialTransaction transaction, List<FinancialTransaction> completedTransactions)
    {
        if (Check(transaction, completedTransactions))
        {
            transaction.Discount = transaction.Discount - _monthlyDiscountSum + MonthLimit;
        }

        return transaction;
    }

    private bool Check(FinancialTransaction transaction, List<FinancialTransaction> completedTransactions)
    {
        List<FinancialTransaction> previousMonthTransactions = completedTransactions
            .Where(t => StringIntoDateTimeConverter.Convert(t).Year == StringIntoDateTimeConverter.Convert(transaction).Year &&
                StringIntoDateTimeConverter.Convert(t).Month == StringIntoDateTimeConverter.Convert(transaction).Month)
            .ToList();

        _monthlyDiscountSum = previousMonthTransactions.Sum(t => t.Discount) + transaction.Discount;

        return _monthlyDiscountSum > 10;
    }
}