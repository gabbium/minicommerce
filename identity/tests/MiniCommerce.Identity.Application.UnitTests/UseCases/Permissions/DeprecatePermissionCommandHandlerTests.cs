using MiniCommerce.Identity.Application.UseCases.Permissions.DeprecatePermission;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Errors;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Repositories;

namespace MiniCommerce.Identity.Application.UnitTests.UseCases.Permissions;

public class DeprecatePermissionCommandHandlerTests
{
    private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeprecatePermissionCommandHandler _handler;

    public DeprecatePermissionCommandHandlerTests()
    {
        _permissionRepositoryMock = new Mock<IPermissionRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeprecatePermissionCommandHandler(_permissionRepositoryMock.Object, _unitOfWorkMock.Object);
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
        _permissionRepositoryMock.VerifyNoOtherCalls();

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.VerifyNoOtherCalls();
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

        _unitOfWorkMock.VerifyNoOtherCalls();
    }
}
