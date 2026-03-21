using Xunit;
using PayrollHelper;

namespace PayrollHelper.Tests
{
    public class HashingTests
    {
        [Fact]
        public void HashPassword_SamePasswords_ReturnSameHashes()
        {
            // Arrange
            string password = "testPassword123";

            // Act
            string hash1 = LoginForm.GetHash(password);
            string hash2 = LoginForm.GetHash(password);

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void HashPassword_DifferentPasswords_ReturnDifferentHashes()
        {
            // Arrange
            string password1 = "Password1";
            string password2 = "Password2";

            // Act
            string hash1 = LoginForm.GetHash(password1);
            string hash2 = LoginForm.GetHash(password2);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void HashPassword_EmptyPassword_ReturnsValidHash()
        {
            // Act
            string hash = LoginForm.GetHash("");

            // Assert
            Assert.False(string.IsNullOrEmpty(hash));
            Assert.Equal(64, hash.Length); // SHA256 hex string length
        }
    }
}
