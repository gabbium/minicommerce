using MiniCommerce.Catalog.Application.Models;

namespace MiniCommerce.Catalog.Application.Features.Products.CreateProduct;

public record CreateProductCommand(string Sku, string Name, decimal Price) : ICommand<ProductResponse>;
