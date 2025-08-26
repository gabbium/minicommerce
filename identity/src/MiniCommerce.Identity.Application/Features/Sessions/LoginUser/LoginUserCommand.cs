using MiniCommerce.Identity.Application.Contracts.Sessions;

namespace MiniCommerce.Identity.Application.Features.Sessions.LoginUser;

public record LoginUserCommand(string Email) : ICommand<TokenResponse>;
