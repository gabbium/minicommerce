using MiniCommerce.Identity.Application.Features.Permissions;
using MiniCommerce.Identity.Application.Features.Permissions.DeprecatePermission;
using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Application.UnitTests.Features.Permissions;

public class DeprecatePermissionCommandHandlerTests
{
    private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
    private readonly DeprecatePermissionCommandHandler _handler;

    public DeprecatePermissionCommandHandlerTests()
    {
        _permissionRepositoryMock = new Mock<IPermissionRepository>();
        _handler = new DeprecatePermissionCommandHandler(_permissionRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenPermissionExists_ThenDeprecatesPermissionAndReturnsIt()
    {
        // Arrange
        var permission = new Permission("catalog:products.list");

        _permissionRepositoryMock
            .Setup(r => r.GetByIdAsync(permission.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        var command = new DeprecatePermissionCommand(permission.Id);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(permission.Id, result.Value.Id);
        Assert.Equal(permission.Code, result.Value.Code);
        Assert.True(permission.Deprecated);

        _permissionRepositoryMock.Verify(r => r.GetByIdAsync(permission.Id, It.IsAny<CancellationToken>()), Times.Once);
        _permissionRepositoryMock.Verify(r => r.UpdateAsync(permission, It.IsAny<CancellationToken>()), Times.Once);
        _permissionRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _permissionRepositoryMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenPermissionDoesNotExist_ThenReturnsNotFound()
    {
        // Arrange
        var permissionId = Guid.NewGuid();

        _permissionRepositoryMock
            .Setup(r => r.GetByIdAsync(permissionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Permission?)null);

        var command = new DeprecatePermissionCommand(permissionId);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(PermissionErrors.NotFound, result.Error);

        _permissionRepositoryMock.Verify(r => r.GetByIdAsync(permissionId, It.IsAny<CancellationToken>()), Times.Once);
        _permissionRepositoryMock.VerifyNoOtherCalls();
    }
}
