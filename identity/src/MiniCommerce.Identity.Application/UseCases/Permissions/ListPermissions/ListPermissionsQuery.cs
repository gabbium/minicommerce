using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Permissions.ListPermissions;

public record ListPermissionsQuery(int PageNumber, int PageSize) : IQuery<PaginatedList<PermissionResponse>>;
