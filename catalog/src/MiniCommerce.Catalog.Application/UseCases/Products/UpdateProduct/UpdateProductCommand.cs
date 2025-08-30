namespace MiniCommerce.Catalog.Application.UseCases.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, decimal Price) : ICommand<ProductResponse>;
