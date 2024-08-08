namespace DiscountsCalculator.Models;

public class FinancialTransaction(string createdAt, string size, string provider)
{
    private string _createdAt = createdAt;
    private string _size = size;
    private string _provider = provider;

    private decimal _price = 0;

    private decimal _discount = 0;

    public string CreatedAt
    {
        get => _createdAt;
    }

    public string Size
    {
        get => _size;
    }

    public string Provider
    {
        get => _provider;
    }

    public decimal Price 
    {
        get => _price;
        set => _price = value;
    }

    public decimal Discount 
    {
        get => _discount;
        set => _discount = value;
    }
}
