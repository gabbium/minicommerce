using MiniCommerce.Catalog.Application.UseCases.Products.GetProductById;
using MiniCommerce.Catalog.Domain.ProductAggregate.Entities;
using MiniCommerce.Catalog.Domain.ProductAggregate.Errors;
using MiniCommerce.Catalog.Domain.ProductAggregate.Repositories;

namespace MiniCommerce.Catalog.Application.UnitTests.UseCases.Products;

public class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenProductExists_ThenReturnsProduct()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);

        _productRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var query = new GetProductByIdQuery(product.Id);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(product.Id, result.Value.Id);
        Assert.Equal(product.Sku, result.Value.Sku);
        Assert.Equal(product.Name, result.Value.Name);
        Assert.Equal(product.Price, result.Value.Price);

        _productRepositoryMock.Verify(r => r.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenProductDoesNotExist_ThenReturnsNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _productRepositoryMock
            .Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        var query = new GetProductByIdQuery(productId);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ProductErrors.NotFound, result.Error);

        _productRepositoryMock.Verify(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.VerifyNoOtherCalls();
    }
}
