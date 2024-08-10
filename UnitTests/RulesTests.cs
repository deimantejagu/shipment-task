namespace UnitTests;

using Xunit;
using DiscountsCalculator.Models;
using DiscountsCalculator.Services;
using DiscountsCalculator.Configs;
using DiscountsCalculator.Rules;

public class RulesTests
{
    [Fact]
    public void MatchSmallestSizePricesRule_AppliesDiscount_WhenSizeS_SetsPriceToDiscountedValue()
    {
        // Arrange
        FinancialTransaction transaction = new FinancialTransaction("2020-07-20", "S", "MR") { Price = 10m };
        MatchSmallestSizePrices rule = new MatchSmallestSizePrices();

        // Act
        FinancialTransaction result = rule.Apply(transaction);

        // Assert
        Assert.Equal(8.5m, result.Discount);
        Assert.Equal(1.50m, result.Price);
    }

    [Fact]
    public void ThirdFreeShipmentRule_AppliesDiscount_ForThirdTransaction()
    {
        // Arrange
        List<FinancialTransaction> completedTransactions = [
            new FinancialTransaction("2020-07-20", "L", "LP") { Price = 6.90m },
            new FinancialTransaction("2020-07-21", "L", "LP") { Price = 6.90m },
        ];
        FinancialTransaction transaction = new FinancialTransaction("2020-07-22", "L", "LP") { Price = 6.90m };


        ThirdFreeShipment rule = new ThirdFreeShipment();

        // Act
        FinancialTransaction result = rule.Apply(transaction, completedTransactions);

        // Assert
        Assert.Equal(6.90m, result.Discount); 
        Assert.Equal(0m, result.Price); 
    }

    [Fact]
    public void ThirdFreeShipmentRule_DoesNotApplyDiscount_ForMoreThanThreeTransactions()
    {
        // Arrange
        List<FinancialTransaction> completedTransactions = [
            new FinancialTransaction("2020-07-20", "L", "LP") { Price = 6.90m },
            new FinancialTransaction("2020-07-21", "L", "LP") { Price = 6.90m },
            new FinancialTransaction("2020-07-22", "L", "LP") { Price = 6.90m },
        ];
        FinancialTransaction transaction = new FinancialTransaction("2020-07-23", "L", "LP") { Price = 6.90m };

        ThirdFreeShipment rule = new ThirdFreeShipment();

        // Act
        FinancialTransaction result = rule.Apply(transaction, completedTransactions);

        // Assert
        Assert.Equal(0m, result.Discount); 
        Assert.Equal(6.90m, result.Price); 
    }

    [Fact]
    public void ThirdFreeShipmentRule_AppliesDiscount_DespiteIrrelevantTransaction()
    {
        // Arrange
        List<FinancialTransaction> completedTransactions = [
            new FinancialTransaction("2020-07-20", "S", "MR") { Price = 2m },
            new FinancialTransaction("2020-07-21", "L", "LP") { Price = 6.90m },
            new FinancialTransaction("2020-07-22", "L", "LP") { Price = 6.90m  },
            new FinancialTransaction("2021-01-01", "L", "LP") { Price = 6.90m },
        ];
        FinancialTransaction transaction = new FinancialTransaction("2020-07-23", "L", "LP") { Price = 6.90m };

        ThirdFreeShipment rule = new ThirdFreeShipment();

        // Act
        FinancialTransaction result = rule.Apply(transaction, completedTransactions);

        // Assert
        Assert.Equal(6.9m, result.Discount);
        Assert.Equal(0, result.Price);
    }

    [Fact]
    public void ThirdFreeShipmentRule_DoesNotApplyDiscount_AcrossDifferentYears()
    {
        // Arrange
        List<FinancialTransaction> completedTransactions = [
            new FinancialTransaction("2020-07-20", "S", "MR") { Price = 2m },
            new FinancialTransaction("2020-07-21", "L", "LP") { Price = 6.90m },
            new FinancialTransaction("2020-07-22", "L", "LP") { Price = 6.90m },
        ];
        FinancialTransaction transaction = new FinancialTransaction("2021-07-23", "L", "LP") { Price = 6.90m };

        ThirdFreeShipment rule = new ThirdFreeShipment();

        // Act
        FinancialTransaction result = rule.Apply(transaction, completedTransactions);

        // Assert
        Assert.Equal(0, result.Discount);
        Assert.Equal(6.9m, result.Price);
    }

    [Fact]
    public void MonthlyDiscountLimitRule_CapsDiscount_WhenTotalExceedsLimit()
    {
        // Arrange
        List<FinancialTransaction> completedTransactions = [
            new FinancialTransaction("2020-07-20", "L", "LP") { Discount = 6.90m },
            new FinancialTransaction("2020-07-21", "L", "LP") { Discount = 2m },
            new FinancialTransaction("2020-07-22", "L", "LP") { Discount = 1m },
        ];
        FinancialTransaction transaction = new FinancialTransaction("2020-07-23", "S", "LP") { Discount = 0.5m };

        MonthlyDiscountLimit rule = new MonthlyDiscountLimit();

        // Act
        FinancialTransaction result = rule.Apply(transaction, completedTransactions);

        // Assert
        Assert.Equal(0.1m, result.Discount);  
    }

    [Fact]
    public void MonthlyDiscountLimitRule_AppliesDiscount_DespiteIrrelevantTransaction()
    {
        // Arrange
        List<FinancialTransaction> completedTransactions = [
            new FinancialTransaction("2020-07-20", "L", "LP") { Discount = 6.90m },
            new FinancialTransaction("2020-07-21", "L", "LP") { Discount = 2m },
            new FinancialTransaction("2021-01-01", "S", "MR") { Discount = 2m },
            new FinancialTransaction("2020-07-22", "L", "LP") { Discount = 1m },
        ];
        FinancialTransaction transaction = new FinancialTransaction("2020-07-23", "S", "LP") { Discount = 0.5m };

        MonthlyDiscountLimit rule = new MonthlyDiscountLimit();

        // Act
        FinancialTransaction result = rule.Apply(transaction, completedTransactions);

        // Assert
        Assert.Equal(0.1m, result.Discount);
    }

    [Fact]
    public void MonthlyDiscountLimitRule_ResetsLimit_ForNewYear()
    {
        // Arrange
        List<FinancialTransaction> completedTransactions = [
            new FinancialTransaction("2020-07-20", "L", "LP") { Discount = 6.90m },
            new FinancialTransaction("2020-07-21", "L", "LP") { Discount = 2m },
            new FinancialTransaction("2020-07-22", "L", "LP") { Discount = 1m },
        ];
        FinancialTransaction transaction = new FinancialTransaction("2021-07-23", "S", "LP") { Discount = 0.5m };

        MonthlyDiscountLimit rule = new MonthlyDiscountLimit();

        // Act
        FinancialTransaction result = rule.Apply(transaction, completedTransactions);

        // Assert
        Assert.Equal(0.5m, result.Discount);
    }
}