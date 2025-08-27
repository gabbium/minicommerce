using MiniCommerce.Identity.Application.Contracts.Users;

namespace MiniCommerce.Identity.Application.Features.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<UserResponse>;
