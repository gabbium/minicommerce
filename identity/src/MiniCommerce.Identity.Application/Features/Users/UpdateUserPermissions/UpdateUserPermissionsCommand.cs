using MiniCommerce.Identity.Application.Contracts.Users;

namespace MiniCommerce.Identity.Application.Features.Users.UpdateUserPermissions;

public record UpdateUserPermissionsCommand(Guid UserId, IReadOnlyList<Guid> PermissionIds) : ICommand<UserResponse>;
