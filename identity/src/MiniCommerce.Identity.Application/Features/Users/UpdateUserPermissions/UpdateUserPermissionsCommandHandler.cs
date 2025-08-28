using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Domain.Aggregates.Users;

namespace MiniCommerce.Identity.Application.Features.Users.UpdateUserPermissions;

public class UpdateUserPermissionsCommandHandler(IUserRepository userRepository) : ICommandHandler<UpdateUserPermissionsCommand, UserResponse>
{
    public async Task<Result<UserResponse>> HandleAsync(UpdateUserPermissionsCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.NotFound);

        user.ReplacePermissions(command.PermissionIds);

        await userRepository.UpdateAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return new UserResponse(user.Id, user.Email);
    }
}
