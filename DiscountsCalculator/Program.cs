using DiscountsCalculator.Services;

namespace DiscountsCalculator;

class Program
{
    static void Main()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../input.txt");
        List<string> transactions = new FileReader(filePath).GetTransactions();

        TransactionsHandler transactionsHandler = new(transactions);
        transactionsHandler.Handle();
    }
}