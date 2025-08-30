using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Users.LoginUser;

public record LoginUserCommand(string Email) : ICommand<TokenResponse>;
