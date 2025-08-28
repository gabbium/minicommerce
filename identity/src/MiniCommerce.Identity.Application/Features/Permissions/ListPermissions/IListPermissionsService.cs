using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.ListPermissions;

public interface IListPermissionsService
{
    Task<PagedList<PermissionResponse>> ListAsync(ListPermissionsQuery query, CancellationToken cancellationToken = default);
}
