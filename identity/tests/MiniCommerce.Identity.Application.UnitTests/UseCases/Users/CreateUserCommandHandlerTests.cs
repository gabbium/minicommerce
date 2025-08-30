using MiniCommerce.Identity.Application.UseCases.Users.CreateUser;
using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;

namespace MiniCommerce.Identity.Application.UnitTests.UseCases.Users;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_CreatesUserAndReturnsIt()
    {
        // Arrange
        var command = new CreateUserCommand("user@minicommerce");

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
        Assert.Equal(command.Email, result.Value.Email);

        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        _userRepositoryMock.VerifyNoOtherCalls();

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.VerifyNoOtherCalls();
    }
}
