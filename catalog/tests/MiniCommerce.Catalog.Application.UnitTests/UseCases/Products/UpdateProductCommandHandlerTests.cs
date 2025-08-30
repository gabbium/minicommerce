using MiniCommerce.Catalog.Application.UseCases.Products.UpdateProduct;
using MiniCommerce.Catalog.Domain.Aggregates.Products.Entities;
using MiniCommerce.Catalog.Domain.Aggregates.Products.Errors;
using MiniCommerce.Catalog.Domain.Aggregates.Products.Repositories;

namespace MiniCommerce.Catalog.Application.UnitTests.UseCases.Products;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenProductExists_ThenUpdatesProductAndReturnsIt()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);

        _productRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
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

        _productRepositoryMock.Verify(r => r.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.Verify(r => r.UpdateAsync(product, It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.VerifyNoOtherCalls();

        _unitOfWork.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenProductDoesNotExist_ThenReturnsNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _productRepositoryMock
            .Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        var command = new UpdateProductCommand(productId, "Wireless Mouse", 79.90m);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ProductErrors.NotFound, result.Error);

        _productRepositoryMock.Verify(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.VerifyNoOtherCalls();

        _unitOfWork.VerifyNoOtherCalls();
    }
}
