namespace DiscountsCalculator;

using DiscountsCalculator.Services;
using DiscountsCalculator.Models;


class Program
{
    static void Main()
    {
        List<string> transactions = new FileReader("input.txt").GetTransactions();

        TransactionsHandler transactionsHandler = new(transactions);
        transactionsHandler.Handle();
    }
}