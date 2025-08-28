using MiniCommerce.Identity.Application.Features.Permissions;
using MiniCommerce.Identity.Application.Features.Permissions.DeletePermission;
using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Application.UnitTests.Features.Permissions;

public class DeletePermissionCommandHandlerTests
{
    private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
    private readonly DeletePermissionCommandHandler _handler;

    public DeletePermissionCommandHandlerTests()
    {
        _permissionRepositoryMock = new Mock<IPermissionRepository>();
        _handler = new DeletePermissionCommandHandler(_permissionRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenPermissionExists_ThenDeletesPermissionAndReturnsSuccess()
    {
        // Arrange
        var permission = new Permission("catalog:products.list");

        _permissionRepositoryMock
            .Setup(r => r.GetByIdAsync(permission.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        var command = new DeletePermissionCommand(permission.Id);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);

        _permissionRepositoryMock.Verify(r => r.GetByIdAsync(permission.Id, It.IsAny<CancellationToken>()), Times.Once);
        _permissionRepositoryMock.Verify(r => r.DeleteAsync(permission, It.IsAny<CancellationToken>()), Times.Once);
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

        var command = new DeletePermissionCommand(permissionId);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(PermissionErrors.NotFound, result.Error);

        _permissionRepositoryMock.Verify(r => r.GetByIdAsync(permissionId, It.IsAny<CancellationToken>()), Times.Once);
        _permissionRepositoryMock.VerifyNoOtherCalls();
    }
}
