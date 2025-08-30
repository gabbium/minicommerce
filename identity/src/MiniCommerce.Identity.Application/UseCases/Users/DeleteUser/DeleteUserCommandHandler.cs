using MiniCommerce.Identity.Domain.Aggregates.Users.Errors;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;

namespace MiniCommerce.Identity.Application.UseCases.Users.DeleteUser;

public class DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(command.Id, cancellationToken);

        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        await userRepository.DeleteAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
