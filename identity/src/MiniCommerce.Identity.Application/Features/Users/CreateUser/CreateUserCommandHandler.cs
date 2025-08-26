using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Domain.Aggregates.Users;

namespace MiniCommerce.Identity.Application.Features.Users.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository) : ICommandHandler<CreateUserCommand, UserResponse>
{
    public async Task<Result<UserResponse>> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = new User(command.Email);

        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return new UserResponse(user.Id, user.Email);
    }
}
