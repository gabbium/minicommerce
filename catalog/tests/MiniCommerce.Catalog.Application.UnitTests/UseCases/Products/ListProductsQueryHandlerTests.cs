using MiniCommerce.Catalog.Application.Abstractions;
using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Application.UseCases.Products.ListProducts;

namespace MiniCommerce.Catalog.Application.UnitTests.UseCases.Products;

public class ListProductsQueryHandlerTests
{
    private readonly Mock<IProductQueries> _productQueries;
    private readonly ListProductsQueryHandler _handler;

    public ListProductsQueryHandlerTests()
    {
        _productQueries = new Mock<IProductQueries>();
        _handler = new ListProductsQueryHandler(_productQueries.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsPaginatedList()
    {
        // Arrange
        var query = new ListProductsQuery(1, 10);

        var expectedProducts = new PaginatedList<ProductResponse>(
            [new(Guid.NewGuid(), "SKU-001", "Bluetooth Headphones", 129.50m)],
            1,
            1,
            10
        );

        _productQueries
            .Setup(s => s.ListAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProducts);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedProducts, result.Value);

        _productQueries.Verify(s => s.ListAsync(query, It.IsAny<CancellationToken>()), Times.Once);
        _productQueries.VerifyNoOtherCalls();
    }
}
