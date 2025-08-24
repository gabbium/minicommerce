namespace MiniCommerce.Catalog.Domain.Entities;

public sealed class Product(string sku, string name, decimal price)
{
    public Guid Id { get; set; }
    public string Sku { get; private set; } = sku;
    public string Name { get; private set; } = name;
    public decimal Price { get; private set; } = price;
    public bool IsActive { get; private set; }
}

