using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Users.ListUsers;

public class ListUsersQueryHandler(IUserQueries listUsersService) : IQueryHandler<ListUsersQuery, PaginatedList<UserResponse>>
{
    public async Task<Result<PaginatedList<UserResponse>>> HandleAsync(ListUsersQuery query, CancellationToken cancellationToken = default)
    {
        return await listUsersService.ListAsync(query, cancellationToken);
    }
}
