namespace UnitTests;

using System;
using Xunit;
using DiscountsCalculator.Models;
using DiscountsCalculator.Services;

public class FileReaderTests
{
    private const string TestFilePath = "test.txt";

    // [Fact]
    // public void ReadValidFile_ReturnsCorrectTransactions()
    // {
    //     // Arrange
    //     List<FinancialTransaction> expectedTransactions = new []
    //     {
    //         new FinancialTransaction(new DateTime(2023, 8, 5), "L", "LP"),
    //         new FinancialTransaction(new DateTime(2023, 8, 4), "M", "MR")
    //     };
        
    //     // Act
    //     List<FinancialTransaction> actualTransactions = new FileReader(TestFilePath).GetTransactions();

    //     // Assert
    //     Assert.Equal(expectedTransactions, actualTransactions);
    // }

    [Fact]
    public void FileNotFound_ThrowsException(){
        // Arrange
        var fileReader = new FileReader("nonexistent.txt");

        // Act and Assert
        Assert.Throws<FileNotFoundException>(() => fileReader.GetTransactions());
    }

    [Fact]
    public void ExtractsCorrectDataFromLine()
    {
        // Arrange
        var line = "2023-08-05 L LP";

        // Act
        var transaction = FileReader.ParseTransactionLine(line);

        // Assert
        Assert.Equal(new DateTime(2023, 8, 5), transaction.CreatedAt);
        Assert.Equal("L", transaction.Size);
        Assert.Equal("LP", transaction.Provider);
    }

    [Fact]
    public void ValidatesDateFormat()
    {
        // Arrange
        var invalidLine = "invalid date L LP";

        // Act and Assert
        Assert.Throws<FormatException>(() => FileReader.ParseTransactionLine(invalidLine));
    }
}