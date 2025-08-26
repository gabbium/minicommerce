using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Domain.UnitTests.Aggregates.Permissions;

public class PermissionTests
{
    [Fact]
    public void Ctor_CreatesPermission()
    {
        // Arrange
        var code = "catalog:products.list";

        // Act
        var permission = new Permission(code);

        // Assert
        Assert.NotEqual(Guid.Empty, permission.Id);
        Assert.Equal(code, permission.Code);
        Assert.False(permission.Deprecated);
    }

    [Fact]
    public void Deprecate_SetsDeprecatedToTrue()
    {
        // Arrange
        var permission = new Permission("catalog:products.list");

        // Act
        permission.Deprecate();

        // Assert
        Assert.True(permission.Deprecated);
    }
}
