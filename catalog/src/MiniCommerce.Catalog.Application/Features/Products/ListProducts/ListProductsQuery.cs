using MiniCommerce.Catalog.Application.Common.Models;
using MiniCommerce.Catalog.Application.Contracts.Products;

namespace MiniCommerce.Catalog.Application.Features.Products.ListProducts;

public record ListProductsQuery(int Page, int PageSize) : IQuery<PagedList<ProductResponse>>;

