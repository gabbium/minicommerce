using MiniCommerce.Identity.Application.Contracts.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.GetPermissionById;

public record GetPermissionByIdQuery(Guid Id) : IQuery<PermissionResponse>;
