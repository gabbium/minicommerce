using MiniCommerce.Identity.Domain.ValueObjects;

namespace MiniCommerce.Identity.Infrastructure.Security;

public sealed class PermissionDefinition(string name, params Role[] roles)
{
    public string Name { get; } = name;
    public IReadOnlyList<Role> Roles { get; } = roles;
}
