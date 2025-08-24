using MiniCommerce.Catalog.Domain.Entities;

namespace MiniCommerce.Catalog.Domain.UnitTests.Entities;

public class ProductTests
{
    [Fact]
    public void Constructor_ThenInstantiate()
    {
        // Arrange
        var sku = "SKU-001";
        var name = "Bluetooth Headphones";
        var price = 129.50m;

        // Act
        var product = new Product(sku, name, price);

        // Assert
        Assert.Equal(sku, product.Sku);
        Assert.Equal(name, product.Name);
        Assert.Equal(price, product.Price);
    }
}
