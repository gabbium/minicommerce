using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Application.Features.Users.ListUsers;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.Persistence.Services;

public class ListUsersService(AppDbContext context) : IListUsersService
{
    public async Task<PagedList<UserResponse>> ListAsync(ListUsersQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = context.Users.AsQueryable();

        var totalCount = await queryable.CountAsync(cancellationToken);

        var users = await queryable
            .OrderBy(u => u.Email)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = new List<UserResponse>();

        foreach (var user in users)
        {
            mapped.Add(new UserResponse(user.Id, user.Email));
        }

        return new PagedList<UserResponse>(mapped, totalCount, query.Page, query.PageSize);
    }
}
