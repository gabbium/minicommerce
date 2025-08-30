namespace MiniCommerce.Catalog.Application.Contracts;

public record ProductResponse(Guid Id, string Sku, string Name, decimal Price);
