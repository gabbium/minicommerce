using MiniCommerce.Catalog.Application.Abstractions;
using MiniCommerce.Catalog.Application.Contracts;

namespace MiniCommerce.Catalog.Application.UseCases.Products.ListProducts;

public class ListProductsQueryHandler(IProductQueries listProductsService) : IQueryHandler<ListProductsQuery, PaginatedList<ProductResponse>>
{
    public async Task<Result<PaginatedList<ProductResponse>>> HandleAsync(ListProductsQuery query, CancellationToken cancellationToken = default)
    {
        return await listProductsService.ListAsync(query, cancellationToken);
    }
}

