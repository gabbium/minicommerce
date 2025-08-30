using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Users.ListUsers;

namespace MiniCommerce.Identity.Application.Abstractions;

public interface IUserQueries
{
    Task<PaginatedList<UserResponse>> ListAsync(ListUsersQuery query, CancellationToken cancellationToken = default);
}
