using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<User?> FirstOrDefaultAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
    {
        return context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(specification.Criteria, cancellationToken);
    }

    public async Task<IReadOnlyList<User>> ListAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .AsNoTracking()
            .Where(specification.Criteria)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<User>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
    {
        return context.Users
            .AsNoTracking()
            .CountAsync(specification.Criteria, cancellationToken);
    }

    public Task<bool> AnyAsync(ISpecification<User> specification, CancellationToken cancellationToken = default)
    {
        return context.Users
            .AsNoTracking()
            .AnyAsync(specification.Criteria, cancellationToken);
    }

    public async Task AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        context.Users.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
    {
        context.Users.Remove(entity);
        return Task.CompletedTask;
    }
}
