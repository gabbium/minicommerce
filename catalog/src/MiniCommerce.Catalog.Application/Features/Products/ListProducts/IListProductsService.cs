using MiniCommerce.Catalog.Application.Common.Models;
using MiniCommerce.Catalog.Application.Contracts.Products;

namespace MiniCommerce.Catalog.Application.Features.Products.ListProducts;

public interface IListProductsService
{
    Task<PagedList<ProductResponse>> ListAsync(ListProductsQuery query, CancellationToken cancellationToken = default);
}

