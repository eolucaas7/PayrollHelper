using Xunit;
using PayrollHelper;

namespace PayrollHelper.Tests
{
    public class FormattingTests
    {
        [Theory]
        [InlineData("+7 (903) 123-45-67", "+79031234567")]
        [InlineData("9031234567", "+79031234567")]
        [InlineData("12345", "+712345")]
        [InlineData("89031234567", "+79031234567")]
        [InlineData("+79031234567", "+79031234567")]
        public void FormatPhoneNumber_ReturnsExpectedFormat(string input, string expected)
        {
            // Act
            string result = FormattingHelper.FormatPhoneNumber(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
