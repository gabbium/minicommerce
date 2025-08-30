using MiniCommerce.Catalog.Application.Abstractions;
using MiniCommerce.Catalog.Application.UseCases.Products.ListProducts;
using MiniCommerce.Catalog.Domain.Aggregates.Products.Entities;
using MiniCommerce.Catalog.Domain.Aggregates.Products.Repositories;
using MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers.Fixtures;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.Persistence.Queries;

[Collection(nameof(TestCollection))]
public class ProductQueriesTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly IProductQueries _queries =
        fixture.GetRequiredService<IProductQueries>();

    private readonly IProductRepository _repository =
        fixture.GetRequiredService<IProductRepository>();

    private readonly IUnitOfWork _unitOfWork =
        fixture.GetRequiredService<IUnitOfWork>();

    [Fact]
    public async Task ProductsArePagedAndOrderedCorrectly()
    {
        // Arrange
        var product1 = new Product("SKU-001", "Bluetooth Headphones", 129.50m);
        var product2 = new Product("SKU-002", "Wireless Mouse", 79.90m);
        var product3 = new Product("SKU-003", "Mechanical Keyboard", 299.00m);

        await _repository.AddAsync(product1);
        await _repository.AddAsync(product2);
        await _repository.AddAsync(product3);
        await _unitOfWork.SaveChangesAsync();

        var query = new ListProductsQuery(1, 2);

        // Act
        var result = await _queries.ListAsync(query);

        // Assert
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(3, result.TotalItems);
        Assert.Equal(2, result.TotalPages);
        Assert.Equal(2, result.Items.Count);

        Assert.Collection(
            result.Items,
            first =>
            {
                Assert.Equal(product1.Id, first.Id);
                Assert.Equal(product1.Sku, first.Sku);
                Assert.Equal(product1.Name, first.Name);
                Assert.Equal(product1.Price, first.Price);
            },
            second =>
            {
                Assert.Equal(product2.Id, second.Id);
                Assert.Equal(product2.Sku, second.Sku);
                Assert.Equal(product2.Name, second.Name);
                Assert.Equal(product2.Price, second.Price);
            }
        );
    }
}
