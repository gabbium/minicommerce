using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Users.ListUsers;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.Persistence.Queries;

public class UserQueries(AppDbContext context) : IUserQueries
{
    public async Task<PaginatedList<UserResponse>> ListAsync(ListUsersQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = context.Users.AsQueryable();

        var totalItems = await queryable.CountAsync(cancellationToken);

        var users = await queryable
            .AsNoTracking()
            .OrderBy(u => u.Email)
            .ThenBy(u => u.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(u => new UserResponse(u.Id, u.Email))
            .ToListAsync(cancellationToken);

        return new PaginatedList<UserResponse>(users, totalItems, query.PageNumber, query.PageSize);
    }
}
