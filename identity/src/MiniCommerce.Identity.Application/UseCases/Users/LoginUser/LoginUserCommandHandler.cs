using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;
using MiniCommerce.Identity.Domain.Aggregates.Users.Specifications;

namespace MiniCommerce.Identity.Application.UseCases.Users.LoginUser;

public class LoginUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService) : ICommandHandler<LoginUserCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> HandleAsync(LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.FirstOrDefaultAsync(new UserByEmailSpec(command.Email), cancellationToken);

        if (user is null)
        {
            user = new User(command.Email);
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var accessToken = jwtTokenService.CreateAccessToken(user);

        return new TokenResponse(accessToken);
    }
}
