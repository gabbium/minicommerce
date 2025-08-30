using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Users.ListUsers;

public record ListUsersQuery(int PageNumber, int PageSize) : IQuery<PaginatedList<UserResponse>>;
