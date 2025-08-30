using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Permissions.ListPermissions;

public class ListPermissionsQueryHandler(IPermissionQueries listPermissionsService) : IQueryHandler<ListPermissionsQuery, PaginatedList<PermissionResponse>>
{
    public async Task<Result<PaginatedList<PermissionResponse>>> HandleAsync(ListPermissionsQuery query, CancellationToken cancellationToken = default)
    {
        return await listPermissionsService.ListAsync(query, cancellationToken);
    }
}
