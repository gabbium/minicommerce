using MiniCommerce.Identity.Domain.Aggregates.Permissions.Errors;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Repositories;

namespace MiniCommerce.Identity.Application.UseCases.Permissions.DeletePermission;

public class DeletePermissionCommandHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeletePermissionCommand>
{
    public async Task<Result> HandleAsync(DeletePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var permission = await permissionRepository.GetByIdAsync(command.Id, cancellationToken);

        if (permission is null)
            return Result.Failure(PermissionErrors.NotFound);

        await permissionRepository.DeleteAsync(permission, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
