using MiniCommerce.Identity.Application.Interfaces;
using MiniCommerce.Identity.Application.Models;
using MiniCommerce.Identity.Domain.Entities;
using MiniCommerce.Identity.Domain.Interfaces;

namespace MiniCommerce.Identity.Application.Commands.Login;

public class LoginCommandHandler(IUserRepository userRepository, ITokenService tokenProvider) : ICommandHandler<LoginCommand, AuthResponse>
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
