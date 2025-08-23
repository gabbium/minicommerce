using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Models;
using MiniCommerce.Identity.Domain.Abstractions;
using MiniCommerce.Identity.Domain.Entities;

namespace MiniCommerce.Identity.Application.Commands.Auth.Login;

public class LoginCommandHandler(IUserRepository userRepository, IJwtTokenService tokenProvider) : ICommandHandler<LoginCommand, AuthResponse>
{
    public async Task<Result<AuthResponse>> HandleAsync(LoginCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user is null)
        {
            user = new User(command.Email);
            await userRepository.AddAsync(user, cancellationToken);
            await userRepository.SaveChangesAsync(cancellationToken);
        }

        var token = tokenProvider.CreateAccessToken(user);

        return new AuthResponse(new(user.Email), new(token));
    }
}
