using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.ListPermissions;

public record ListPermissionsQuery(int Page, int PageSize) : IQuery<PagedList<PermissionResponse>>;
