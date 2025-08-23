using MiniCommerce.Identity.Application.Models;

namespace MiniCommerce.Identity.Application.Commands.Login;

public record LoginCommand(string Email) : ICommand<AuthResponse>;
