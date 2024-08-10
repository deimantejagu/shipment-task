namespace DiscountsCalculator.Services;

public class FileReader(string fileName)
{
    public List<string> GetTransactions()
    {
       List<string> transactions = [];

        try
        {
            using (StreamReader sr = new(fileName))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    transactions.Add(line);
                }
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"Error reading file: {e.Message}");
            throw;
        }

        return transactions;
    }
}
