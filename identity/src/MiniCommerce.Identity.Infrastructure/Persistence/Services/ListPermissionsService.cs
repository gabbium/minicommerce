using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Application.Features.Permissions.ListPermissions;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.Persistence.Services;

public class ListPermissionsService(AppDbContext context) : IListPermissionsService
{
    public async Task<PagedList<PermissionResponse>> ListAsync(ListPermissionsQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = context.Permissions.AsQueryable();

        var totalCount = await queryable.CountAsync(cancellationToken);

        var permissions = await queryable
            .OrderBy(u => u.Code)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = new List<PermissionResponse>();

        foreach (var permission in permissions)
        {
            mapped.Add(new PermissionResponse(permission.Id, permission.Code, permission.Deprecated));
        }

        return new PagedList<PermissionResponse>(mapped, totalCount, query.Page, query.PageSize);
    }
}
