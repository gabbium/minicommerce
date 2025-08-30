using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Users.CreateUser;

public record CreateUserCommand(string Email) : ICommand<UserResponse>;
