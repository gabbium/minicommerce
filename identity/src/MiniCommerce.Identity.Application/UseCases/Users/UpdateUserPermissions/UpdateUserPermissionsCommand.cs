using MiniCommerce.Identity.Application.Contracts;

namespace MiniCommerce.Identity.Application.UseCases.Users.UpdateUserPermissions;

public record UpdateUserPermissionsCommand(Guid UserId, IReadOnlyList<Guid> PermissionIds) : ICommand<UserResponse>;
