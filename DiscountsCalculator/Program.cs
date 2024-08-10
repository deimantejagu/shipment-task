using DiscountsCalculator.Services;

namespace DiscountsCalculator;

class Program
{
    static void Main()
    {
        List<string> transactions = new FileReader("input.txt").GetTransactions();

        TransactionsHandler transactionsHandler = new(transactions);
        transactionsHandler.Handle();
    }
}