using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Users.ListUsers;

namespace MiniCommerce.Identity.Application.UnitTests.UseCases.Users;

public class ListUsersQueryHandlerTests
{
    private readonly Mock<IUserQueries> _userQueriesMock;
    private readonly ListUsersQueryHandler _handler;

    public ListUsersQueryHandlerTests()
    {
        _userQueriesMock = new Mock<IUserQueries>();
        _handler = new ListUsersQueryHandler(_userQueriesMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsPagedUsers()
    {
        // Arrange
        var query = new ListUsersQuery(1, 10);

        var expectedUsers = new PaginatedList<UserResponse>(
            [new(Guid.NewGuid(), "user@minicommerce")],
            1,
            1,
            10
        );

        _userQueriesMock
            .Setup(s => s.ListAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUsers);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedUsers, result.Value);

        _userQueriesMock.Verify(s => s.ListAsync(query, It.IsAny<CancellationToken>()), Times.Once);
        _userQueriesMock.VerifyNoOtherCalls();
    }
}
