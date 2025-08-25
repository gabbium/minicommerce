using MiniCommerce.Catalog.Application.Features.Products;
using MiniCommerce.Catalog.Application.Features.Products.UpdateProduct;
using MiniCommerce.Catalog.Domain.Abstractions;
using MiniCommerce.Catalog.Domain.Entities;

namespace MiniCommerce.Catalog.Application.UnitTests.Features.Products;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new UpdateProductCommandHandler(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenProductExists_ThenUpdatesProductAndReturnsIt()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var command = new UpdateProductCommand(product.Id, "Wireless Mouse", 79.90m);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(product.Id, result.Value.Id);
        Assert.Equal(product.Sku, result.Value.Sku);
        Assert.Equal(command.Name, result.Value.Name);
        Assert.Equal(command.Price, result.Value.Price);

        _productRepositoryMock.Verify(x => x.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.Verify(x => x.UpdateAsync(product, It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenProductDoesNotExist_ThenReturnsNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        var command = new UpdateProductCommand(productId, "Wireless Mouse", 79.90m);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ProductErrors.NotFound, result.Error);

        _productRepositoryMock.Verify(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.VerifyNoOtherCalls();
    }
}
