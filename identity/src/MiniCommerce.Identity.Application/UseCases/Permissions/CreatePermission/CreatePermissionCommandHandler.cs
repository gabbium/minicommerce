using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Repositories;

namespace MiniCommerce.Identity.Application.UseCases.Permissions.CreatePermission;

public class CreatePermissionCommandHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreatePermissionCommand, PermissionResponse>
{
    public async Task<Result<PermissionResponse>> HandleAsync(CreatePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var permission = new Permission(command.Code);

        await permissionRepository.AddAsync(permission, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new PermissionResponse(permission.Id, permission.Code, permission.Deprecated);
    }
}
