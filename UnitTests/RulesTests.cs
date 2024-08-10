namespace UnitTests;

using System;
using Xunit;
using DiscountsCalculator.Models;
using DiscountsCalculator.Services;
using DiscountsCalculator.Configs;
using DiscountsCalculator.Rules;

public class RulesTests
{
    [Fact]
    public void CalculateDiscount_SizeS_SetsPriceToLowest()
    {
        // Arrange
        FinancialTransaction transaction = new FinancialTransaction("2020-07-20", "S", "MR") { Price = 10 };

        MatchSmallestSizePrices rule = new MatchSmallestSizePrices(transaction);

        // Act
        decimal discount = rule.CalculateDiscount();

        // Assert
        Assert.Equal(0.5m, discount);
        Assert.Equal(1.50m, transaction.Price);
    }

    [Fact]
    public void CalculateDiscount_SetFreeShipment()
    {
        // Arrange
        FinancialTransaction transaction = new FinancialTransaction("2020-07-20", "L", "LP") { Price = 10 };
        int counter = 3;
        ThirdFreeShipment rule = new ThirdFreeShipment(transaction, counter);

        // Act
        decimal discount = rule.CalculateDiscount();

        // Assert
        Assert.Equal(6.90m, discount); 
        Assert.Equal(0m, transaction.Price); 
    }

    [Fact]
    public void CalculateDiscount_NoFreeShipment()
    {
        // Arrange
        FinancialTransaction transaction = new FinancialTransaction("2020-07-20", "L", "LP") { Price = 10 };
        int counter = 1;
        ThirdFreeShipment rule = new ThirdFreeShipment(transaction, counter);

        // Act
        decimal discount = rule.CalculateDiscount();

        // Assert
        Assert.Equal(0m, discount); 
        Assert.Equal(6.90m, transaction.Price); 
    }

    [Fact]
    public void CalculateDiscount_DiscountExceedsLimit()
    {
        // Arrange
        FinancialTransaction transaction = new FinancialTransaction("2020-07-20", "L", "LP") { Discount = 0.5m };
        decimal monthlyDiscountSum = 10.4m;
        MonthlyDiscountLimit rule = new MonthlyDiscountLimit(transaction, monthlyDiscountSum);

        // Act
        decimal discount = rule.CalculateDiscount();

        // Assert
        Assert.Equal(0.1m, discount);  
    }
}