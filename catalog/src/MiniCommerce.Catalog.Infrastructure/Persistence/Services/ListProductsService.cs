using MiniCommerce.Catalog.Application.Common.Models;
using MiniCommerce.Catalog.Application.Contracts.Products;
using MiniCommerce.Catalog.Application.Features.Products.ListProducts;
using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Catalog.Infrastructure.Persistence.Services;

public class ListProductsService(AppDbContext context) : IListProductsService
{
    public async Task<PagedList<ProductResponse>> ListAsync(ListProductsQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = context.Products.AsQueryable();

        var totalCount = await queryable.CountAsync(cancellationToken);

        var products = await queryable
            .OrderBy(u => u.Sku)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = new List<ProductResponse>();

        foreach (var product in products)
        {
            mapped.Add(new ProductResponse(product.Id, product.Sku, product.Name, product.Price));
        }

        return new PagedList<ProductResponse>(mapped, totalCount, query.Page, query.PageSize);
    }
}
