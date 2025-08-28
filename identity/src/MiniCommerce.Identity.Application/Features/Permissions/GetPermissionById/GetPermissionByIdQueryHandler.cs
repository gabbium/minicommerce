using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.GetPermissionById;

public class GetPermissionByIdQueryHandler(IPermissionRepository permissionRepository) : IQueryHandler<GetPermissionByIdQuery, PermissionResponse>
{
    public async Task<Result<PermissionResponse>> HandleAsync(GetPermissionByIdQuery query, CancellationToken cancellationToken = default)
    {
        var permission = await permissionRepository.GetByIdAsync(query.Id, cancellationToken);

        if (permission is null)
            return Result.Failure<PermissionResponse>(PermissionErrors.NotFound);

        return new PermissionResponse(permission.Id, permission.Code, permission.Deprecated);
    }
}
