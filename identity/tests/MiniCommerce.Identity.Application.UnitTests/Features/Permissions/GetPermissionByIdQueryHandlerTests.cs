using MiniCommerce.Identity.Application.Features.Permissions;
using MiniCommerce.Identity.Application.Features.Permissions.GetPermissionById;
using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Application.UnitTests.Features.Permissions;

public class GetPermissionByIdQueryHandlerTests
{
    private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
    private readonly GetPermissionByIdQueryHandler _handler;

    public GetPermissionByIdQueryHandlerTests()
    {
        _permissionRepositoryMock = new Mock<IPermissionRepository>();
        _handler = new GetPermissionByIdQueryHandler(_permissionRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenPermissionExists_ThenReturnsPermission()
    {
        // Arrange
        var permission = new Permission("catalog:products.list");

        _permissionRepositoryMock
            .Setup(r => r.GetByIdAsync(permission.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(permission);

        var query = new GetPermissionByIdQuery(permission.Id);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(permission.Id, result.Value.Id);
        Assert.Equal(permission.Code, result.Value.Code);
        Assert.False(permission.Deprecated);

        _permissionRepositoryMock.Verify(r => r.GetByIdAsync(permission.Id, It.IsAny<CancellationToken>()), Times.Once);
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

        var query = new GetPermissionByIdQuery(permissionId);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(PermissionErrors.NotFound, result.Error);

        _permissionRepositoryMock.Verify(r => r.GetByIdAsync(permissionId, It.IsAny<CancellationToken>()), Times.Once);
        _permissionRepositoryMock.VerifyNoOtherCalls();
    }
}
