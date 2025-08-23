using MiniCommerce.Identity.Domain.Entities;
using MiniCommerce.Identity.Domain.Interfaces;

namespace MiniCommerce.Identity.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return context.Users
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
