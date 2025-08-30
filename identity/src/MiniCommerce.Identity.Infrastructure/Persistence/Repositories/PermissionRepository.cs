using MiniCommerce.Identity.Domain.Aggregates.Permissions.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Repositories;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.Persistence.Repositories;

public class PermissionRepository(AppDbContext context) : IPermissionRepository
{
    public Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Permissions
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<Permission?> FirstOrDefaultAsync(ISpecification<Permission> specification, CancellationToken cancellationToken = default)
    {
        return context.Permissions
            .AsNoTracking()
            .FirstOrDefaultAsync(specification.Criteria, cancellationToken);
    }

    public async Task<IReadOnlyList<Permission>> ListAsync(ISpecification<Permission> specification, CancellationToken cancellationToken = default)
    {
        return await context.Permissions
            .AsNoTracking()
            .Where(specification.Criteria)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Permission>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Permissions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(ISpecification<Permission> specification, CancellationToken cancellationToken = default)
    {
        return context.Permissions
            .AsNoTracking()
            .CountAsync(specification.Criteria, cancellationToken);
    }

    public Task<bool> AnyAsync(ISpecification<Permission> specification, CancellationToken cancellationToken = default)
    {
        return context.Permissions
            .AsNoTracking()
            .AnyAsync(specification.Criteria, cancellationToken);
    }

    public async Task AddAsync(Permission entity, CancellationToken cancellationToken = default)
    {
        await context.Permissions.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(Permission entity, CancellationToken cancellationToken = default)
    {
        context.Permissions.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Permission entity, CancellationToken cancellationToken = default)
    {
        context.Permissions.Remove(entity);
        return Task.CompletedTask;
    }
}
