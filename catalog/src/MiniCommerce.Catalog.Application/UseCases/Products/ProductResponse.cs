namespace MiniCommerce.Catalog.Application.UseCases.Products;

public record ProductResponse(Guid Id, string Sku, string Name, decimal Price);
