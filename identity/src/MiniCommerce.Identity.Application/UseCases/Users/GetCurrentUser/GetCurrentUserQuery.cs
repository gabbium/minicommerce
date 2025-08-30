using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Users.GetCurrentUser;

public record GetCurrentUserQuery : IQuery<UserResponse>;
