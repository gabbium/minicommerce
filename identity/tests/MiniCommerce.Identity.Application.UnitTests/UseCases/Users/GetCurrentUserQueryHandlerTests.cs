using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.UseCases.Users.GetCurrentUser;

namespace MiniCommerce.Identity.Application.UnitTests.UseCases.Users;

public class GetCurrentUserQueryHandlerTests
{
    private readonly Mock<IUserContext> _userContextMock;
    private readonly GetCurrentUserQueryHandler _handler;

    public GetCurrentUserQueryHandlerTests()
    {
        _userContextMock = new Mock<IUserContext>();
        _handler = new GetCurrentUserQueryHandler(_userContextMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var email = "user@minicommerce";

        _userContextMock
            .Setup(c => c.UserId)
            .Returns(userId);

        _userContextMock
            .Setup(c => c.Email)
            .Returns(email);

        var query = new GetCurrentUserQuery();

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(userId, result.Value.Id);
        Assert.Equal(email, result.Value.Email);

        _userContextMock.Verify(c => c.UserId, Times.Once);
        _userContextMock.Verify(c => c.Email, Times.Once);
        _userContextMock.VerifyNoOtherCalls();
    }
}
