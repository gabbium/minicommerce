using MiniCommerce.Catalog.Domain.Aggregates.Products;

namespace MiniCommerce.Catalog.Domain.UnitTests.Aggregates.Products;

public class ProductTests
{
    [Fact]
    public void Ctor_CreatesProduct()
    {
        // Arrange
        var sku = "SKU-001";
        var name = "Bluetooth Headphones";
        var price = 129.50m;

        // Act
        var product = new Product(sku, name, price);

        // Assert
        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Equal(sku, product.Sku);
        Assert.Equal(name, product.Name);
        Assert.Equal(price, product.Price);
    }

    [Fact]
    public void ChangeName_UpdatesProductName()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);
        var newName = "Wireless Mouse";

        // Act
        product.ChangeName(newName);

        // Assert
        Assert.Equal(newName, product.Name);
    }

    [Fact]
    public void ChangePrice_UpdatesProductPrice()
    {
        // Arrange
        var product = new Product("SKU-001", "Bluetooth Headphones", 129.50m);
        var newPrice = 79.90m;

        // Act
        product.ChangePrice(newPrice);

        // Assert
        Assert.Equal(newPrice, product.Price);
    }
}
