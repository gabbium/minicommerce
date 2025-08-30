using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Permissions.ListPermissions;

namespace MiniCommerce.Identity.Application.UnitTests.UseCases.Permissions;

public class ListPermissionsQueryHandlerTests
{
    private readonly Mock<IPermissionQueries> _permissionQueriesMock;
    private readonly ListPermissionsQueryHandler _handler;

    public ListPermissionsQueryHandlerTests()
    {
        _permissionQueriesMock = new Mock<IPermissionQueries>();
        _handler = new ListPermissionsQueryHandler(_permissionQueriesMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsPagedPermissions()
    {
        // Arrange
        var query = new ListPermissionsQuery(1, 10);

        var expectedPermissions = new PaginatedList<PermissionResponse>(
            [new(Guid.NewGuid(), "catalog:products.list", false)],
            1,
            1,
            10
        );

        _permissionQueriesMock
            .Setup(s => s.ListAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPermissions);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedPermissions, result.Value);

        _permissionQueriesMock.Verify(s => s.ListAsync(query, It.IsAny<CancellationToken>()), Times.Once);
        _permissionQueriesMock.VerifyNoOtherCalls();
    }
}
