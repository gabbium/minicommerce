using MiniCommerce.Identity.Domain.Aggregates.Users;

namespace MiniCommerce.Identity.Application.Features.Users.DeleteUser;

public class DeleteUserCommandHandler(IUserRepository userRepository) : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(command.Id, cancellationToken);

        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        await userRepository.DeleteAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
