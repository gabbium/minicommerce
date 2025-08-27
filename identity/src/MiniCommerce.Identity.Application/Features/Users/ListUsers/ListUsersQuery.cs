using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Users;

namespace MiniCommerce.Identity.Application.Features.Users.ListUsers;

public record ListUsersQuery(int Page, int PageSize) : IQuery<PagedList<UserResponse>>;
