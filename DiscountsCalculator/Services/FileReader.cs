namespace DiscountsCalculator.Services;

public class FileReader(string fileName)
{
    private string _fileName = fileName;

    public List<string> GetTransactions()
    {
       List<string> transactions = [];

        try
        {
            using (StreamReader sr = new(_fileName))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {   
                        transactions.Add(line);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Error reading file: {e.Message}");
                    }
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
