using MiniCommerce.Catalog.Application.Models;

namespace MiniCommerce.Catalog.Application.Features.Products.ListProducts;

public class ListProductsQueryHandler(IListProductsService listProductsService) : IQueryHandler<ListProductsQuery, PagedList<ProductResponse>>
{
    public async Task<Result<PagedList<ProductResponse>>> HandleAsync(ListProductsQuery query, CancellationToken cancellationToken = default)
    {
        return await listProductsService.ListAsync(query, cancellationToken);
    }
}

