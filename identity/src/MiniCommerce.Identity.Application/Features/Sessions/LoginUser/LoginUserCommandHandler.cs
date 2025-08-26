using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts.Sessions;
using MiniCommerce.Identity.Domain.Aggregates.Users;

namespace MiniCommerce.Identity.Application.Features.Sessions.LoginUser;

public class LoginUserCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService) : ICommandHandler<LoginUserCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> HandleAsync(LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user is null)
        {
            user = new User(command.Email);
            await userRepository.AddAsync(user, cancellationToken);
            await userRepository.SaveChangesAsync(cancellationToken);
        }

        var accessToken = jwtTokenService.CreateAccessToken(user);

        return new TokenResponse(accessToken);
    }
}
