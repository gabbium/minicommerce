using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;

namespace MiniCommerce.Identity.Application.UseCases.Users.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateUserCommand, UserResponse>
{
    public async Task<Result<UserResponse>> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = new User(command.Email);

        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserResponse(user.Id, user.Email);
    }
}
