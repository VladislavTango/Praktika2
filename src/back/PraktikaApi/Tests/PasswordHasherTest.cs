using PraktikaApplication.Static;

namespace PasswordHasherTests
{
    public class PasswordHasherTests
    {
        [Fact]
        public void HashPassword()
        {
            // Arrange
            var password = "test";

            // Act
            var hashedPassword = PasswordHasher.HashPassword(password);

            // Assert
            Assert.NotNull(hashedPassword);
            Assert.True(hashedPassword.Length > 0 && hashedPassword != null);
        }

        [Fact]
        public void VerifyPassword_true()
        {
            // Arrange
            var password = "test";
            var hashedPassword = PasswordHasher.HashPassword(password);

            // Act
            var result = PasswordHasher.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_false()
        {
            // Arrange
            var password = "test";
            var wrongPassword = "aaaaa";
            var hashedPassword = PasswordHasher.HashPassword(password);

            // Act
            var result = PasswordHasher.VerifyPassword(wrongPassword, hashedPassword);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_WithNull()
        {
            // Arrange
            var password = "TestPassword123";

            // Act
            var result = PasswordHasher.VerifyPassword(password, null);

            // Assert
            Assert.False(result);
        }
    }
}