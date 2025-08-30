using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Errors;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Repositories;

namespace MiniCommerce.Identity.Application.UseCases.Permissions.DeprecatePermission;

public class DeprecatePermissionCommandHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeprecatePermissionCommand, PermissionResponse>
{
    public async Task<Result<PermissionResponse>> HandleAsync(DeprecatePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var permission = await permissionRepository.GetByIdAsync(command.Id, cancellationToken);

        if (permission is null)
            return Result.Failure<PermissionResponse>(PermissionErrors.NotFound);

        permission.Deprecate();

        await permissionRepository.UpdateAsync(permission, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new PermissionResponse(permission.Id, permission.Code, permission.Deprecated);
    }
}
