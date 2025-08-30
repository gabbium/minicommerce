using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<UserResponse>;
