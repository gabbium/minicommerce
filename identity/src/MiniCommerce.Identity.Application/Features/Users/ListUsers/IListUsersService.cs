using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Users;

namespace MiniCommerce.Identity.Application.Features.Users.ListUsers;

public interface IListUsersService
{
    Task<PagedList<UserResponse>> ListAsync(ListUsersQuery query, CancellationToken cancellationToken = default);
}
