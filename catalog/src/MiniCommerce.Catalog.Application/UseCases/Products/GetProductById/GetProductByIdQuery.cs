using MiniCommerce.Catalog.Application.Contracts;

namespace MiniCommerce.Catalog.Application.UseCases.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
