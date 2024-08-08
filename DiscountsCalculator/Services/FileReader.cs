namespace DiscountsCalculator.Services;

using System;
using System.Globalization;
using System.IO;
using DiscountsCalculator.Models;

public class FileReader(string fileName)
{
    private string _fileName = fileName;

    public List<FinancialTransaction> GetTransactions()
    {
        List<FinancialTransaction> transactions = [];

        try
        {
            using (StreamReader sr = new(_fileName))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {   
                        FinancialTransaction transaction = ParseTransactionLine(line);
                        transactions.Add(transaction);
                    }
                    catch (FormatException)
                    {
                        // Console.WriteLine($"{line} ignored");
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

    public static FinancialTransaction ParseTransactionLine(string line)
    {
        string[] splitedLine = line.Split(' ');
        DateTime createdAt = DateTime.ParseExact(splitedLine[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        string size = splitedLine[1];
        string provider = splitedLine[2];

        return new FinancialTransaction(createdAt, size, provider);
    }
}
