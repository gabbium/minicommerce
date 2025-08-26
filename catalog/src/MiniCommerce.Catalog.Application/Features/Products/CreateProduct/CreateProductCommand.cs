using MiniCommerce.Catalog.Application.Contracts.Products;

namespace MiniCommerce.Catalog.Application.Features.Products.CreateProduct;

public record CreateProductCommand(string Sku, string Name, decimal Price) : ICommand<ProductResponse>;
