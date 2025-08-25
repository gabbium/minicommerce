namespace MiniCommerce.Catalog.Application.Features.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand;
