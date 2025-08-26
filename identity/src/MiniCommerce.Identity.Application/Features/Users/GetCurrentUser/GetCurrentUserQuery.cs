using MiniCommerce.Identity.Application.Contracts.Users;

namespace MiniCommerce.Identity.Application.Features.Users.GetCurrentUser;

public record GetCurrentUserQuery : IQuery<UserResponse>;
