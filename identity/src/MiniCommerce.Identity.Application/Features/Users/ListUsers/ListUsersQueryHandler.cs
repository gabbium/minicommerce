using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Users;

namespace MiniCommerce.Identity.Application.Features.Users.ListUsers;

public class ListUsersQueryHandler(IListUsersService listUsersService) : IQueryHandler<ListUsersQuery, PagedList<UserResponse>>
{
    public async Task<Result<PagedList<UserResponse>>> HandleAsync(ListUsersQuery query, CancellationToken cancellationToken = default)
    {
        return await listUsersService.ListAsync(query, cancellationToken);
    }
}
