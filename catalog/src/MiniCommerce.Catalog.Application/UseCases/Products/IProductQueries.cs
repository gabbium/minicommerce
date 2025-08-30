using MiniCommerce.Catalog.Application.UseCases.Products.ListProducts;

namespace MiniCommerce.Catalog.Application.UseCases.Products;

public interface IProductQueries
{
    Task<PaginatedList<ProductResponse>> ListAsync(ListProductsQuery query, CancellationToken cancellationToken = default);
}

