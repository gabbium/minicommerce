namespace MiniCommerce.Catalog.Domain.Aggregates.Products.Entities;

public sealed class Product(string sku, string name, decimal price) : IAggregateRoot
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Sku { get; private set; } = sku;
    public string Name { get; private set; } = name;
    public decimal Price { get; private set; } = price;

    public void ChangeName(string name) => Name = name;

    public void ChangePrice(decimal price) => Price = price;
}

