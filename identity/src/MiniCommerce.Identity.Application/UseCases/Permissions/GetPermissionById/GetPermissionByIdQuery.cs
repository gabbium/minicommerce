using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Permissions.GetPermissionById;

public record GetPermissionByIdQuery(Guid Id) : IQuery<PermissionResponse>;
