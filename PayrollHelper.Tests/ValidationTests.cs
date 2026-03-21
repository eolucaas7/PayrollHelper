using Xunit;
using PayrollHelper;

namespace PayrollHelper.Tests
{
    public class ValidationTests
    {
        [Theory]
        [InlineData("Иванов Иван", true)]
        [InlineData("John Doe", true)]
        [InlineData("Салтыков-Щедрин", true)]
        [InlineData("12", false)]
        [InlineData("Иван123", false)]
        [InlineData("@#$", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void ValidateFullName_ReturnsExpectedResult(string? name, bool expected)
        {
            // Act
            bool result = ValidationHelper.IsValidFullName(name);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("100", true)]
        [InlineData("100.50", true)]
        [InlineData("100,50", true)]
        [InlineData("0.01", true)]
        [InlineData("-100", false)]
        [InlineData("abc", false)]
        [InlineData("100.50.60", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void ValidateAmount_ReturnsExpectedResult(string? amount, bool expected)
        {
            // Act
            bool result = ValidationHelper.IsValidAmount(amount);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("13", true)]
        [InlineData("20.5", true)]
        [InlineData("0.01", true)]
        [InlineData("100", true)]
        [InlineData("0", false)]
        [InlineData("101", false)]
        [InlineData("-5", false)]
        [InlineData("abc", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void ValidateTaxRate_ReturnsExpectedResult(string? rate, bool expected)
        {
            // Act
            bool result = ValidationHelper.IsValidTaxRate(rate);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
