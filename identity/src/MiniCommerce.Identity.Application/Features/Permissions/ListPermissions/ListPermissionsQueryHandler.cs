using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.ListPermissions;

public class ListPermissionsQueryHandler(IListPermissionsService listPermissionsService) : IQueryHandler<ListPermissionsQuery, PagedList<PermissionResponse>>
{
    public async Task<Result<PagedList<PermissionResponse>>> HandleAsync(ListPermissionsQuery query, CancellationToken cancellationToken = default)
    {
        return await listPermissionsService.ListAsync(query, cancellationToken);
    }
}
