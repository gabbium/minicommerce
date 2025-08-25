using MiniCommerce.Catalog.Application.Models;

namespace MiniCommerce.Catalog.Application.Features.Products.ListProducts;

public record ListProductsQuery(int Page, int PageSize) : IQuery<PagedList<ProductResponse>>;

