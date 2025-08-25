using MiniCommerce.Identity.Application.Models;

namespace MiniCommerce.Identity.Application.Features.Auth.Login;

public record LoginCommand(string Email) : ICommand<AuthResponse>;
