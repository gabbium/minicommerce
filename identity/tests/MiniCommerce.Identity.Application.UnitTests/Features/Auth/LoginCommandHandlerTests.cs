using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Features.Auth.Login;
using MiniCommerce.Identity.Domain.Abstractions;
using MiniCommerce.Identity.Domain.Entities;

namespace MiniCommerce.Identity.Application.UnitTests.Features.Auth;

public class LoginCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _handler = new LoginCommandHandler(_userRepositoryMock.Object, _jwtTokenServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenUserExists_ThenReturnsAuthResponse()
    {
        // Arrange
        var command = new LoginCommand("user@minicommerce");
        var user = new User(command.Email);
        var accessToken = "valid.access.token";

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _jwtTokenServiceMock
            .Setup(x => x.CreateAccessToken(user))
            .Returns(accessToken);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(accessToken, result.Value.Token.AccessToken);

        _userRepositoryMock.Verify(x => x.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
        _jwtTokenServiceMock.Verify(x => x.CreateAccessToken(user), Times.Once);

        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        _userRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandleAsync_WhenUserNotExists_ThenCreatesAndReturnsAuthResponse()
    {
        // Arrange
        var command = new LoginCommand("user@minicommerce");
        var accessToken = "valid.access.token";

        _jwtTokenServiceMock
            .Setup(x => x.CreateAccessToken(It.IsAny<User>()))
            .Returns(accessToken);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(accessToken, result.Value.Token.AccessToken);

        _userRepositoryMock.Verify(x => x.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _jwtTokenServiceMock.Verify(x => x.CreateAccessToken(It.IsAny<User>()), Times.Once);
    }
}
