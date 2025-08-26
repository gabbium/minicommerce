using MiniCommerce.Identity.Application.Contracts.Users;

namespace MiniCommerce.Identity.Application.Features.Users.CreateUser;

public record CreateUserCommand(string Email) : ICommand<UserResponse>;
