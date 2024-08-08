namespace DiscountsCalculator.Services;

using DiscountsCalculator.Models;
using DiscountsCalculator.Configs;

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

        if(IsTransactionValid(_transaction)){

            return _transaction;
        }

        return null;
    }

    private FinancialTransaction ParseTransactionLine(string transaction)
    {
        string[] splitedLine = transaction.Split(' ');
        string createdAt = splitedLine[0];
        string size = splitedLine[1];
        string provider = splitedLine[2];

        return new FinancialTransaction(createdAt, size, provider);
    }

    private bool IsTransactionValid(FinancialTransaction transaction){
        foreach(var provider in ProvidersData.Providers)
        {
            if((provider.Provider == transaction.Provider) && (provider.Size == transaction.Size))
            {
                return true;
            }
        }

        return  false;
    }
}
