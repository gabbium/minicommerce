using MiniCommerce.Catalog.Application.Contracts;

namespace MiniCommerce.Catalog.Application.UseCases.Products.CreateProduct;

public record CreateProductCommand(string Sku, string Name, decimal Price) : ICommand<ProductResponse>;
