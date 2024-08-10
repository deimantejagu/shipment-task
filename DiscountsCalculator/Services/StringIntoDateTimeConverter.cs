using DiscountsCalculator.Models;
using System.Globalization;

namespace DiscountsCalculator.Services;

public class StringIntoDateTimeConverter()
{
    public static DateTime Convert(FinancialTransaction transaction)
    {
        try 
        {
            return DateTime.ParseExact(transaction.CreatedAt, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        } 
        catch (FormatException e)
        {
            Console.WriteLine($"Error parsing date: {e.Message}");
            
            throw;
        }
    }
}
