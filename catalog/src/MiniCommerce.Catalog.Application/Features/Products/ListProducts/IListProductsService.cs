using MiniCommerce.Catalog.Application.Models;

namespace MiniCommerce.Catalog.Application.Features.Products.ListProducts;

public interface IListProductsService
{
    Task<PagedList<ProductResponse>> ListAsync(ListProductsQuery query, CancellationToken cancellationToken = default);
}

