using MiniCommerce.Catalog.Application.UseCases.Products.CreateProduct;
using MiniCommerce.Catalog.Domain.ProductAggregate.Entities;
using MiniCommerce.Catalog.Domain.ProductAggregate.Repositories;

namespace MiniCommerce.Catalog.Application.UnitTests.UseCases.Products;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _unitOfWork.Object);
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

        _productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
        _productRepositoryMock.VerifyNoOtherCalls();

        _unitOfWork.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.VerifyNoOtherCalls();
    }
}
