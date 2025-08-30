using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Permissions.CreatePermission;

public record CreatePermissionCommand(string Code) : ICommand<PermissionResponse>;
