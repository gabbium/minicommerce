using MiniCommerce.Catalog.Application.Common.Models;
using MiniCommerce.Catalog.Application.Contracts.Products;

namespace MiniCommerce.Catalog.Application.Features.Products.ListProducts;

public class ListProductsQueryHandler(IListProductsService listProductsService) : IQueryHandler<ListProductsQuery, PagedList<ProductResponse>>
{
    public async Task<Result<PagedList<ProductResponse>>> HandleAsync(ListProductsQuery query, CancellationToken cancellationToken = default)
    {
        return await listProductsService.ListAsync(query, cancellationToken);
    }
}

