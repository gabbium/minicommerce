using MiniCommerce.Identity.Domain.Entities;
using MiniCommerce.Identity.Domain.ValueObjects;

namespace MiniCommerce.Identity.Domain.UnitTests.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_ThenInstantiate()
    {
        // Arrange
        var email = "user@minicommerce";
        var role = Role.User;

        // Act
        var user = new User(email, role);

        // Assert
        Assert.Equal(email, user.Email);
        Assert.Equal(role, user.Role);
    }
}
