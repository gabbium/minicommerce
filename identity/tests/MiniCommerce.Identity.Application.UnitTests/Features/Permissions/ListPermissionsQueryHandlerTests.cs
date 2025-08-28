using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Application.Features.Permissions.ListPermissions;

namespace MiniCommerce.Identity.Application.UnitTests.Features.Permissions;

public class ListPermissionsQueryHandlerTests
{
    private readonly Mock<IListPermissionsService> _listPermissionsServiceMock;
    private readonly ListPermissionsQueryHandler _handler;

    public ListPermissionsQueryHandlerTests()
    {
        _listPermissionsServiceMock = new Mock<IListPermissionsService>();
        _handler = new ListPermissionsQueryHandler(_listPermissionsServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsPagedPermissions()
    {
        // Arrange
        var query = new ListPermissionsQuery(1, 10);

        var expectedPermissions = new PagedList<PermissionResponse>(
            [new(Guid.NewGuid(), "catalog:products.list", false)],
            1,
            1,
            10
        );

        _listPermissionsServiceMock
            .Setup(s => s.ListAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPermissions);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedPermissions, result.Value);

        _listPermissionsServiceMock.Verify(s => s.ListAsync(query, It.IsAny<CancellationToken>()), Times.Once);
        _listPermissionsServiceMock.VerifyNoOtherCalls();
    }
}
