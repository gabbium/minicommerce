using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.DeprecatePermission;

public class DeprecatePermissionCommandHandler(IPermissionRepository permissionRepository) : ICommandHandler<DeprecatePermissionCommand, PermissionResponse>
{
    public async Task<Result<PermissionResponse>> HandleAsync(DeprecatePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var permission = await permissionRepository.GetByIdAsync(command.Id, cancellationToken);

        if (permission is null)
            return Result.Failure<PermissionResponse>(PermissionErrors.NotFound);

        permission.Deprecate();

        await permissionRepository.UpdateAsync(permission, cancellationToken);
        await permissionRepository.SaveChangesAsync(cancellationToken);

        return new PermissionResponse(permission.Id, permission.Code, permission.Deprecated);
    }
}
