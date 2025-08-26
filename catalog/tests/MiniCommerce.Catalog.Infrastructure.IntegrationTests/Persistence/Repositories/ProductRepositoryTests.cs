using MiniCommerce.Catalog.Domain.Aggregates.Products;
using MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.Persistence.Repositories;

[Collection(nameof(TestCollection))]
public class ProductRepositoryTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly IProductRepository _repository =
        fixture.GetRequiredService<IProductRepository>();

    [Fact]
    public async Task ProductIsCreatedAndLoadedCorrectly()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);

        // Act
        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByIdAsync(product.Id);
        Assert.NotNull(retrieved);
        Assert.Equal(product.Id, retrieved.Id);
        Assert.Equal(product.Sku, retrieved.Sku);
        Assert.Equal(product.Name, retrieved.Name);
        Assert.Equal(product.Price, retrieved.Price);
    }

    [Fact]
    public async Task ProductIsUpdatedCorrectly()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);
        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        // Act
        product.ChangeName("Wireless Mouse");
        product.ChangePrice(79.90m);

        await _repository.UpdateAsync(product);
        await _repository.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByIdAsync(product.Id);
        Assert.NotNull(retrieved);
        Assert.Equal(product.Name, retrieved.Name);
        Assert.Equal(product.Price, retrieved.Price);
    }

    [Fact]
    public async Task ProductIsDeletedCorrectly()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);
        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(product);
        await _repository.SaveChangesAsync();

        // Assert
        var retrieved = await _repository.GetByIdAsync(product.Id);
        Assert.Null(retrieved);
    }
}
