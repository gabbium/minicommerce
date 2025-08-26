using MiniCommerce.Identity.Application.Contracts.Permissions;

namespace MiniCommerce.Identity.Application.Features.Permissions.CreatePermission;

public record CreatePermissionCommand(string Code) : ICommand<PermissionResponse>;
