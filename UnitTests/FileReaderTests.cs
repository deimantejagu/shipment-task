using Xunit;
using DiscountsCalculator.Models;
using DiscountsCalculator.Services;

namespace UnitTests;

public class FileReaderTests
{
    [Fact]
    public void GetTransactions_ValidFile_ReturnsListOfLines()
    {
        // Arrange
        const string fileName = "valid_transactions.txt"; 
        StreamWriter fileWriter = new StreamWriter(fileName); 
        fileWriter.WriteLine("2023-08-08 S LP 10.00");
        fileWriter.WriteLine("2023-08-09 M MR 5.00");
        fileWriter.Close();

        FileReader reader = new FileReader(fileName);

        // Act
        List<string> transactions = reader.GetTransactions();

        // Assert
        Assert.Equal(2, transactions.Count);
        Assert.Equal("2023-08-08 S LP 10.00", transactions[0]);
        Assert.Equal("2023-08-09 M MR 5.00", transactions[1]);

        File.Delete(fileName);
    }

    [Fact]
    public void GetTransactions_NonExistentFile_ThrowsFileNotFoundException()
    {
        // Arrange
        const string fileName = "non_existing_file.txt";
        FileReader reader = new FileReader(fileName);

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => reader.GetTransactions());
    }
}