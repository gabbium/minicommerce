using MiniCommerce.Catalog.Application.Models;

namespace MiniCommerce.Catalog.Application.Features.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
