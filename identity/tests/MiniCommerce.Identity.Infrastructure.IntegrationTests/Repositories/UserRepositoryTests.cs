using MiniCommerce.Identity.Domain.Abstractions;
using MiniCommerce.Identity.Domain.Entities;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.Repositories;

[Collection(nameof(TestCollection))]
public class UserRepositoryTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly IUserRepository _repository =
        fixture.GetRequiredService<IUserRepository>();

    [Fact]
    public async Task UserIsCreatedAndLoadedCorrectly()
    {
        // Arrange
        var user = new User("user@minicommerce");

        // Act
        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByEmailAsync(user.Email);

        Assert.NotNull(retrieved);
        Assert.Equal(user.Id, retrieved.Id);
        Assert.Equal(user.Email, retrieved.Email);
    }
}
