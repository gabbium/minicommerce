using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;

namespace MiniCommerce.Identity.Domain.UnitTests.Aggregates.Users.Entities;

public class UserTests
{
    [Fact]
    public void Ctor_CreatesUser()
    {
        // Arrange
        var email = "user@minicommerce";

        // Act
        var user = new User(email);

        // Assert
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(email, user.Email);
    }
}
