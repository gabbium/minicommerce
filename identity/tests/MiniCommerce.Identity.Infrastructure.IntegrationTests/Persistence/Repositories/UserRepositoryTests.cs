using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers.Fixtures;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.Persistence.Repositories;

[Collection(nameof(TestCollection))]
public class UserRepositoryTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly IUserRepository _repository =
        fixture.GetRequiredService<IUserRepository>();

    private readonly IUnitOfWork _unitOfWork =
        fixture.GetRequiredService<IUnitOfWork>();

    [Fact]
    public async Task UserIsCreatedAndLoadedCorrectly()
    {
        // Arrange
        var user = new User("user@minicommerce");

        // Act
        await _repository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByIdAsync(user.Id);
        Assert.NotNull(retrieved);
        Assert.Equal(user.Id, retrieved.Id);
        Assert.Equal(user.Email, retrieved.Email);
    }

    [Fact]
    public async Task UserIsUpdatedCorrectly()
    {
        // Arrange
        var user = new User("user@minicommerce");
        await _repository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Act
        await _repository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByIdAsync(user.Id);
        Assert.NotNull(retrieved);
        Assert.Equal(user.Id, retrieved.Id);
        Assert.Equal(user.Email, retrieved.Email);
    }

    [Fact]
    public async Task UserIsDeletedCorrectly()
    {
        // Arrange
        var user = new User("user@minicommerce");
        await _repository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByIdAsync(user.Id);
        Assert.Null(retrieved);
    }
}
