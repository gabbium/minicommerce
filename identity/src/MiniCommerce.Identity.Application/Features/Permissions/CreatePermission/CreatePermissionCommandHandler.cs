using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.CreatePermission;

public class CreatePermissionCommandHandler(IPermissionRepository permissionRepository) : ICommandHandler<CreatePermissionCommand, PermissionResponse>
{
    public async Task<Result<PermissionResponse>> HandleAsync(CreatePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var permission = new Permission(command.Code);

        await permissionRepository.AddAsync(permission, cancellationToken);
        await permissionRepository.SaveChangesAsync(cancellationToken);

        return new PermissionResponse(permission.Id, permission.Code, permission.Deprecated);
    }
}
