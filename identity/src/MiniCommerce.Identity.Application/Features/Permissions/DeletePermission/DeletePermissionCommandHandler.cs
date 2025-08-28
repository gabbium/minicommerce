using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.DeletePermission;

public class DeletePermissionCommandHandler(IPermissionRepository permissionRepository) : ICommandHandler<DeletePermissionCommand>
{
    public async Task<Result> HandleAsync(DeletePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var permission = await permissionRepository.GetByIdAsync(command.Id, cancellationToken);

        if (permission is null)
            return Result.Failure(PermissionErrors.NotFound);

        await permissionRepository.DeleteAsync(permission, cancellationToken);
        await permissionRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
