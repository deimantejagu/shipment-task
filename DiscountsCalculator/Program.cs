namespace DiscountsCalculator;

using DiscountsCalculator.Services;

class Program
{
    static void Main()
    {
        List<string> transactions = new FileReader("input.txt").GetTransactions();

        TransactionsHandler transactionsHandler = new(transactions);
        transactionsHandler.Handle();
    }
}