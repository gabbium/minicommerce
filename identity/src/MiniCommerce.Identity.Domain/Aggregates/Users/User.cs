namespace MiniCommerce.Identity.Domain.Aggregates.Users;

public sealed class User(string email)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Email { get; private set; } = email;

    private readonly List<UserPermission> _permissions = [];
    public IReadOnlyCollection<UserPermission> Permissions => _permissions.AsReadOnly();

    public void ReplacePermissions(IEnumerable<Guid> permissionIds)
    {
        _permissions.Clear();

        foreach (var permissionId in permissionIds.Distinct())
        {
            _permissions.Add(new UserPermission(Id, permissionId));
        }
    }
}
