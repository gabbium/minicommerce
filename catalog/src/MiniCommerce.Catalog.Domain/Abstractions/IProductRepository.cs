using MiniCommerce.Catalog.Domain.Entities;

namespace MiniCommerce.Catalog.Domain.Abstractions;

public interface IProductRepository
{
    Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
