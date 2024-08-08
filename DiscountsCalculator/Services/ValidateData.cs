namespace DiscountsCalculator.Services;

using DiscountsCalculator.Models;
using Microsoft.VisualBasic;

public class ValidateData(string transaction)
{
    private FinancialTransaction _transaction;

    public FinancialTransaction Validate(){
        try
        {   
            _transaction = ParseTransactionLine(transaction);
        }
        catch
        {
            return null;
        }

        return _transaction;
    }

    private FinancialTransaction ParseTransactionLine(string line)
    {
        string[] splitedLine = line.Split(' ');
        string createdAt = splitedLine[0];
        string size = splitedLine[1];
        string provider = splitedLine[2];

        return new FinancialTransaction(createdAt, size, provider);
    }
}
