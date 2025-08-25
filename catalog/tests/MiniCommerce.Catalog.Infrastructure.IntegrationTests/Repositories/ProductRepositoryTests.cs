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
    public async Task AddAsync_ThenCreatesProductSuccessfully()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);

        // Act
        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        // Assert
        var found = await _repository.GetByIdAsync(product.Id);

        Assert.NotNull(found);
        Assert.Equal(product.Id, found.Id);
        Assert.Equal(product.Sku, found.Sku);
        Assert.Equal(product.Name, found.Name);
        Assert.Equal(product.Price, found.Price);
        Assert.Equal(product.IsActive, found.IsActive);
    }

    [Fact]
    public async Task GetByIdAsync_WhenNonExistentProduct_ThenReturnsNull()
    {
        // Act
        var found = await _repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(found);
    }

    [Fact]
    public async Task GetBySkuAsync_ThenReturnsProduct()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);
        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        // Act
        var found = await _repository.GetBySkuAsync(product.Sku);

        // Assert
        Assert.NotNull(found);
        Assert.Equal(product.Id, found.Id);
        Assert.Equal(product.Sku, found.Sku);
        Assert.Equal(product.Name, found.Name);
        Assert.Equal(product.Price, found.Price);
        Assert.Equal(product.IsActive, found.IsActive);
    }
}
