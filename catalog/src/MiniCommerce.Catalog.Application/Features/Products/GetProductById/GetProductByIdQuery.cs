using MiniCommerce.Catalog.Application.Contracts.Products;

namespace MiniCommerce.Catalog.Application.Features.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
