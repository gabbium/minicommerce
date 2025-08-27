using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Domain.Aggregates.Users;

public sealed class UserPermission(Guid UserId, Guid PermissionId)
{
    public Guid UserId { get; private set; } = UserId;
    public User User { get; private set; } = default!;

    public Guid PermissionId { get; private set; } = PermissionId;
    public Permission Permission { get; private set; } = default!;
}
