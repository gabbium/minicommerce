using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Features.Sessions.LoginUser;
using MiniCommerce.Identity.Domain.Aggregates.Users;

namespace MiniCommerce.Identity.Application.UnitTests.Features.Sessions;

public class LoginUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _jwtTokenServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenUserExists_ThenReturnsAuthResponse()
    {
        // Arrange
        var command = new LoginUserCommand("user@minicommerce");
        var user = new User(command.Email);
        var accessToken = "valid.access.token";

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _jwtTokenServiceMock
            .Setup(s => s.CreateAccessToken(user))
            .Returns(accessToken);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(accessToken, result.Value.AccessToken);

        _userRepositoryMock.Verify(r => r.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.VerifyNoOtherCalls();

        _jwtTokenServiceMock.Verify(s => s.CreateAccessToken(user), Times.Once);
        _jwtTokenServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenUserNotExists_ThenCreatesUserAndReturnsAuthResponse()
    {
        // Arrange
        var command = new LoginUserCommand("user@minicommerce");
        var accessToken = "valid.access.token";

        _jwtTokenServiceMock
            .Setup(s => s.CreateAccessToken(It.IsAny<User>()))
            .Returns(accessToken);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(accessToken, result.Value.AccessToken);

        _userRepositoryMock.Verify(r => r.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.VerifyNoOtherCalls();

        _jwtTokenServiceMock.Verify(s => s.CreateAccessToken(It.IsAny<User>()), Times.Once);
        _jwtTokenServiceMock.VerifyNoOtherCalls();
    }
}
