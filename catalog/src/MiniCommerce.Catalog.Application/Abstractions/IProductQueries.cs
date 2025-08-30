using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Application.UseCases.Products.ListProducts;

namespace MiniCommerce.Catalog.Application.Abstractions;

public interface IProductQueries
{
    Task<PaginatedList<ProductResponse>> ListAsync(ListProductsQuery query, CancellationToken cancellationToken = default);
}

