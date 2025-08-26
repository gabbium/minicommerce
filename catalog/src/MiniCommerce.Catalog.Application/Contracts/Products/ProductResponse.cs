namespace MiniCommerce.Catalog.Application.Contracts.Products;

public record ProductResponse(Guid Id, string Sku, string Name, decimal Price);
