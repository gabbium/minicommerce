using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.UseCases.Users.LoginUser;
using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;
using MiniCommerce.Identity.Domain.Aggregates.Users.Specifications;

namespace MiniCommerce.Identity.Application.UnitTests.UseCases.Users;

public class LoginUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _jwtTokenServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenUserExists_ThenReturnsAuthResponse()
    {
        // Arrange
        var command = new LoginUserCommand("user@minicommerce");
        var user = new User(command.Email);
        var accessToken = "valid.access.token";

        _userRepositoryMock
            .Setup(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _jwtTokenServiceMock
            .Setup(s => s.CreateAccessToken(user))
            .Returns(accessToken);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(accessToken, result.Value.AccessToken);

        _userRepositoryMock.Verify(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpec>(), It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.VerifyNoOtherCalls();

        _unitOfWorkMock.VerifyNoOtherCalls();

        _jwtTokenServiceMock.Verify(s => s.CreateAccessToken(user), Times.Once);
        _jwtTokenServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenUserNotExists_ThenCreatesUserAndReturnsAuthResponse()
    {
        // Arrange
        var command = new LoginUserCommand("user@minicommerce");
        var accessToken = "valid.access.token";

        _userRepositoryMock
            .Setup(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        _jwtTokenServiceMock
            .Setup(s => s.CreateAccessToken(It.IsAny<User>()))
            .Returns(accessToken);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(accessToken, result.Value.AccessToken);

        _userRepositoryMock.Verify(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpec>(), It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.VerifyNoOtherCalls();

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.VerifyNoOtherCalls();

        _jwtTokenServiceMock.Verify(s => s.CreateAccessToken(It.IsAny<User>()), Times.Once);
        _jwtTokenServiceMock.VerifyNoOtherCalls();
    }
}
