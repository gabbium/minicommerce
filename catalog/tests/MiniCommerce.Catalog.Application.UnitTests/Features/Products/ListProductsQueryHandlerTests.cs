using MiniCommerce.Catalog.Application.Features.Products.ListProducts;
using MiniCommerce.Catalog.Application.Models;

namespace MiniCommerce.Catalog.Application.UnitTests.Features.Products;

public class ListProductsQueryHandlerTests
{
    private readonly Mock<IListProductsService> _listProductsServiceMock;
    private readonly ListProductsQueryHandler _handler;

    public ListProductsQueryHandlerTests()
    {
        _listProductsServiceMock = new Mock<IListProductsService>();
        _handler = new ListProductsQueryHandler(_listProductsServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsPagedProducts()
    {
        // Arrange
        var query = new ListProductsQuery(1, 10);

        var expectedProducts = new PagedList<ProductResponse>(
            [new(Guid.NewGuid(), "SKU-001", "Bluetooth Headphones", 129.50m)],
            1,
            1,
            10
        );

        _listProductsServiceMock
            .Setup(x => x.ListAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProducts);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedProducts, result.Value);

        _listProductsServiceMock.Verify(x => x.ListAsync(query, It.IsAny<CancellationToken>()), Times.Once);
        _listProductsServiceMock.VerifyNoOtherCalls();
    }
}
