using MiniCommerce.Catalog.Application.Features.Products;
using MiniCommerce.Catalog.Application.Features.Products.DeleteProduct;
using MiniCommerce.Catalog.Domain.Abstractions;
using MiniCommerce.Catalog.Domain.Entities;

namespace MiniCommerce.Catalog.Application.UnitTests.Features.Products;

public class DeleteProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new DeleteProductCommandHandler(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProductExistsAndProductIsDelete_ThenReturnsSuccess()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var command = new DeleteProductCommand(productId);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ThenReturnsNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        var command = new DeleteProductCommand(productId);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ProductErrors.NotFound, result.Error);
    }
}
