using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Domain.Aggregates.Users.Errors;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;

namespace MiniCommerce.Identity.Application.UseCases.Users.UpdateUserPermissions;

public class UpdateUserPermissionsCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserPermissionsCommand, UserResponse>
{
    public async Task<Result<UserResponse>> HandleAsync(UpdateUserPermissionsCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.NotFound);

        user.ReplacePermissions(command.PermissionIds);

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserResponse(user.Id, user.Email);
    }
}
