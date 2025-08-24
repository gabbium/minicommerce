using MiniCommerce.Catalog.Domain.Abstractions;
using MiniCommerce.Catalog.Domain.Entities;
using MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.Repositories;

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
        var retrieved = await _repository.GetBySkuAsync(product.Sku);

        Assert.NotNull(retrieved);
        Assert.Equal(product.Id, retrieved.Id);
        Assert.Equal(product.Sku, retrieved.Sku);
        Assert.Equal(product.Name, retrieved.Name);
        Assert.Equal(product.Price, retrieved.Price);
        Assert.Equal(product.IsActive, retrieved.IsActive);
    }
}
