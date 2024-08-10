namespace DiscountsCalculator.Services;

using DiscountsCalculator.Models;
using System.Globalization;

public class StringIntoDateTimeConverter()
{
    public static DateTime Convert(FinancialTransaction transaction)
    {
        try 
        {
            return DateTime.ParseExact(transaction.CreatedAt, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        } 
        catch
        {
            throw;
        }
    }
}
