using MiniCommerce.Catalog.Application.Abstractions;
using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Application.UseCases.Products.ListProducts;
using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Catalog.Infrastructure.Persistence.Queries;

public class ProductQueries(AppDbContext context) : IProductQueries
{
    public async Task<PaginatedList<ProductResponse>> ListAsync(ListProductsQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = context.Products.AsQueryable();

        var totalItems = await queryable.CountAsync(cancellationToken);

        var products = await queryable
            .AsNoTracking()
            .OrderBy(p => p.Sku)
            .ThenBy(p => p.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new ProductResponse(p.Id, p.Sku, p.Name, p.Price))
            .ToListAsync(cancellationToken);

        return new PaginatedList<ProductResponse>(products, totalItems, query.PageNumber, query.PageSize);
    }
}
