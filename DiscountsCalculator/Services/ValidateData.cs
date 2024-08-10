using DiscountsCalculator.Models;
using DiscountsCalculator.Configs;

namespace DiscountsCalculator.Services;

public class ValidateData(string transaction)
{
    private FinancialTransaction? _transaction;

    public FinancialTransaction? Validate()
    {
        try
        {   
            _transaction = ParseTransactionLine(transaction);

            if (IsTransactionValid(_transaction))
            {

                return _transaction;
            }
        }
        catch
        {
            return null;
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

    private bool IsTransactionValid(FinancialTransaction transaction)
    {
        foreach (ProviderInformation provider in ProvidersData.Providers)
        {
            if ((provider.Provider == transaction.Provider) && (provider.Size == transaction.Size))
            {
                return true;
            }
        }

        return  false;
    }
}
