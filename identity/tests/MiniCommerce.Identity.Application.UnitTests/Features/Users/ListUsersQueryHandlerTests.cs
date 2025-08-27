using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Application.Features.Users.ListUsers;

namespace MiniCommerce.Identity.Application.UnitTests.Features.Users;

public class ListUsersQueryHandlerTests
{
    private readonly Mock<IListUsersService> _listUsersServiceMock;
    private readonly ListUsersQueryHandler _handler;

    public ListUsersQueryHandlerTests()
    {
        _listUsersServiceMock = new Mock<IListUsersService>();
        _handler = new ListUsersQueryHandler(_listUsersServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsPagedUsers()
    {
        // Arrange
        var query = new ListUsersQuery(1, 10);

        var expectedUsers = new PagedList<UserResponse>(
            [new(Guid.NewGuid(), "user@minicommerce")],
            1,
            1,
            10
        );

        _listUsersServiceMock
            .Setup(s => s.ListAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUsers);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedUsers, result.Value);

        _listUsersServiceMock.Verify(s => s.ListAsync(query, It.IsAny<CancellationToken>()), Times.Once);
        _listUsersServiceMock.VerifyNoOtherCalls();
    }
}
