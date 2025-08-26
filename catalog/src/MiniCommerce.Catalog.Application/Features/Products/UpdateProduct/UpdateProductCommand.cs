using MiniCommerce.Catalog.Application.Contracts.Products;

namespace MiniCommerce.Catalog.Application.Features.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, decimal Price) : ICommand<ProductResponse>;
