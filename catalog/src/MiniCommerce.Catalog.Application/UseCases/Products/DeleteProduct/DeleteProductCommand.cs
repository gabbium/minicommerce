namespace MiniCommerce.Catalog.Application.UseCases.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand;
