using MiniCommerce.Identity.Domain.Aggregates.Permissions;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.Persistence.Repositories;

[Collection(nameof(TestCollection))]
public class PermissionRepositoryTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly IPermissionRepository _repository =
        fixture.GetRequiredService<IPermissionRepository>();

    [Fact]
    public async Task PermissionIsCreatedAndLoadedCorrectly()
    {
        // Arrange
        var permission = new Permission("catalog:products.list");

        // Act
        await _repository.AddAsync(permission);
        await _repository.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByIdAsync(permission.Id);
        Assert.NotNull(retrieved);
        Assert.Equal(permission.Id, retrieved.Id);
        Assert.Equal(permission.Code, retrieved.Code);
        Assert.Equal(permission.Deprecated, retrieved.Deprecated);
    }

    [Fact]
    public async Task PermissionIsUpdatedCorrectly()
    {
        // Arrange
        var permission = new Permission("catalog:products.list");
        await _repository.AddAsync(permission);
        await _repository.SaveChangesAsync();

        // Act
        permission.Deprecate();

        await _repository.UpdateAsync(permission);
        await _repository.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByIdAsync(permission.Id);
        Assert.NotNull(retrieved);
        Assert.Equal(permission.Deprecated, retrieved.Deprecated);
    }

    [Fact]
    public async Task PermissionIsDeletedCorrectly()
    {
        // Arrange
        var permission = new Permission("catalog:products.list");
        await _repository.AddAsync(permission);
        await _repository.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(permission);
        await _repository.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByIdAsync(permission.Id);
        Assert.Null(retrieved);
    }
}
