using MiniCommerce.Catalog.Domain.ProductAggregate.Entities;
using MiniCommerce.Catalog.Domain.ProductAggregate.Repositories;
using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Catalog.Infrastructure.Persistence.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<Product?> FirstOrDefaultAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
    {
        return context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(specification.Criteria, cancellationToken);
    }

    public async Task AddAsync(Product entity, CancellationToken cancellationToken = default)
    {
        await context.Products.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(Product entity, CancellationToken cancellationToken = default)
    {
        context.Products.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product entity, CancellationToken cancellationToken = default)
    {
        context.Products.Remove(entity);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> AnyAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
    {
        return context.Products
            .AsNoTracking()
            .AnyAsync(specification.Criteria, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> ListAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
    {
        return await context.Products
            .AsNoTracking()
            .Where(specification.Criteria)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
    {
        return context.Products
            .AsNoTracking()
            .CountAsync(specification.Criteria, cancellationToken);
    }
}
