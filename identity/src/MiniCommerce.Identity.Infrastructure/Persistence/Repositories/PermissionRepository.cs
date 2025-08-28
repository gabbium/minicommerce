using MiniCommerce.Identity.Domain.Aggregates.Permissions;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.Persistence.Repositories;

public class PermissionRepository(AppDbContext context) : IPermissionRepository
{
    public Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Permissions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Permission>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await context.Permissions
            .AsNoTracking()
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        await context.Permissions.AddAsync(permission, cancellationToken);
    }

    public Task UpdateAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        context.Permissions.Update(permission);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        context.Permissions.Remove(permission);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
