using MiniCommerce.Catalog.Application.Models;

namespace MiniCommerce.Catalog.Application.Features.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, decimal Price) : ICommand<ProductResponse>;
