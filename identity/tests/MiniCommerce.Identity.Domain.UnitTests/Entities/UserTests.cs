using MiniCommerce.Identity.Domain.Entities;

namespace MiniCommerce.Identity.Domain.UnitTests.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_ThenInstantiate()
    {
        // Arrange
        var email = "user@minicommerce";

        // Act
        var user = new User(email);

        // Assert
        Assert.Equal(email, user.Email);
    }
}
