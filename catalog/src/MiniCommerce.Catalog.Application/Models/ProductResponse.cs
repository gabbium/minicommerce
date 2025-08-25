namespace MiniCommerce.Catalog.Application.Models;

public record ProductResponse(Guid Id, string Sku, string Name, decimal Price);
