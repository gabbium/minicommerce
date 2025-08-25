using MiniCommerce.Catalog.Application.Features.Products.CreateProduct;
using MiniCommerce.Catalog.Domain.Abstractions;
using MiniCommerce.Catalog.Domain.Entities;

namespace MiniCommerce.Catalog.Application.UnitTests.Features.Products;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new CreateProductCommandHandler(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_CreatesProductAndReturnsIt()
    {
        // Arrange
        var command = new CreateProductCommand("SKU-001", "Bluetooth Headphones", 129.50m);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(command.Sku, result.Value.Sku);
        Assert.Equal(command.Name, result.Value.Name);
        Assert.Equal(command.Price, result.Value.Price);

        _productRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.VerifyNoOtherCalls();
    }
}
