using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Permissions.ListPermissions;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.Persistence.Queries;

public class PermissionQueries(AppDbContext context) : IPermissionQueries
{
    public async Task<PaginatedList<PermissionResponse>> ListAsync(ListPermissionsQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = context.Permissions.AsQueryable();

        var totalItems = await queryable.CountAsync(cancellationToken);

        var permissions = await queryable
            .AsNoTracking()
            .OrderBy(p => p.Code)
            .ThenBy(p => p.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new PermissionResponse(p.Id, p.Code, p.Deprecated))
            .ToListAsync(cancellationToken);

        return new PaginatedList<PermissionResponse>(permissions, totalItems, query.PageNumber, query.PageSize);
    }
}
