using MiniCommerce.Identity.Application.Contracts.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.DeprecatePermission;

public record DeprecatePermissionCommand(Guid Id) : ICommand<PermissionResponse>;
