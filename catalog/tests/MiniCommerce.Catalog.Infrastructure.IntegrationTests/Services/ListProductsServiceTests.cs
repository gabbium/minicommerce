using MiniCommerce.Catalog.Application.Features.Products.ListProducts;
using MiniCommerce.Catalog.Domain.Abstractions;
using MiniCommerce.Catalog.Domain.Entities;
using MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.Services;

[Collection(nameof(TestCollection))]
public class ListProductsServiceTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly IListProductsService _service =
        fixture.GetRequiredService<IListProductsService>();

    private readonly IProductRepository _repository =
        fixture.GetRequiredService<IProductRepository>();

    [Fact]
    public async Task ListAsync_WhenProductsExist_ThenReturnsPagedProducts()
    {
        // Arrange
        var product1 = new Product("SKU-001", "Bluetooth Headphones", 129.50m);
        var product2 = new Product("SKU-002", "Wireless Mouse", 79.90m);
        var product3 = new Product("SKU-003", "Mechanical Keyboard", 299.00m);

        await _repository.AddAsync(product1);
        await _repository.AddAsync(product2);
        await _repository.AddAsync(product3);
        await _repository.SaveChangesAsync();

        var query = new ListProductsQuery(1, 2);

        // Act
        var result = await _service.ListAsync(query);

        // Assert
        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(2, result.TotalPages);
        Assert.False(result.HasPreviousPage);
        Assert.True(result.HasNextPage);

        Assert.Equal(product1.Id, result.Items.ElementAt(0).Id);
        Assert.Equal(product1.Sku, result.Items.ElementAt(0).Sku);
        Assert.Equal(product1.Name, result.Items.ElementAt(0).Name);
        Assert.Equal(product1.Price, result.Items.ElementAt(0).Price);

        Assert.Equal(product2.Id, result.Items.ElementAt(1).Id);
        Assert.Equal(product2.Sku, result.Items.ElementAt(1).Sku);
        Assert.Equal(product2.Name, result.Items.ElementAt(1).Name);
        Assert.Equal(product2.Price, result.Items.ElementAt(1).Price);

    }
}
