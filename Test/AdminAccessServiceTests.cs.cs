using System;
using System.Collections.Generic;
using System.Text;
using WorldForge.Services;

namespace Test
{
    public class AdminAccessServiceTests
    {
        [Fact]
        public void IsAdmin_ReturnsTrue_WhenUserIsLoggedInAndAdmin()
        {
            // Arrange
            var service = new AdminAccessService();

            // Act
            var result = service.IsAdmin("true", "true");

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("false", "true")]
        [InlineData("true", "false")]
        [InlineData(null, "true")]
        [InlineData("true", null)]
        [InlineData(null, null)]
        public void IsAdmin_ReturnsFalse_WhenUserIsNotAllowed(string? isLoggedIn, string? isAdmin)
        {
            // Arrange
            var service = new AdminAccessService();

            // Act
            var result = service.IsAdmin(isLoggedIn, isAdmin);

            // Assert
            Assert.False(result);
        }
    }
}
