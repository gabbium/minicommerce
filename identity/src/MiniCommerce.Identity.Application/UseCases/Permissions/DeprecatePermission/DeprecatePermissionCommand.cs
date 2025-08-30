using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Permissions.DeprecatePermission;

public record DeprecatePermissionCommand(Guid Id) : ICommand<PermissionResponse>;
