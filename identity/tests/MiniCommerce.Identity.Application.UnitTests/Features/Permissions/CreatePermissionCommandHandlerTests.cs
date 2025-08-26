using MiniCommerce.Identity.Application.Features.Permissions.CreatePermission;
using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Application.UnitTests.Features.Permissions;

public class CreatePermissionCommandHandlerTests
{
    private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
    private readonly CreatePermissionCommandHandler _handler;

    public CreatePermissionCommandHandlerTests()
    {
        _permissionRepositoryMock = new Mock<IPermissionRepository>();
        _handler = new CreatePermissionCommandHandler(_permissionRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_CreatesPermissionAndReturnsIt()
    {
        // Arrange
        var command = new CreatePermissionCommand("catalog:products.list");

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
        Assert.Equal(command.Code, result.Value.Code);
        Assert.False(result.Value.Deprecated);

        _permissionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Permission>(), It.IsAny<CancellationToken>()), Times.Once);
        _permissionRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _permissionRepositoryMock.VerifyNoOtherCalls();
    }
}
