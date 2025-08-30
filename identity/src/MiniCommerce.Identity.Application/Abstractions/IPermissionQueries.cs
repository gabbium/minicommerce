using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Permissions.ListPermissions;

namespace MiniCommerce.Identity.Application.Abstractions;

public interface IPermissionQueries
{
    Task<PaginatedList<PermissionResponse>> ListAsync(ListPermissionsQuery query, CancellationToken cancellationToken = default);
}
