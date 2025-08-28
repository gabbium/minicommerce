namespace MiniCommerce.Identity.Application.Contracts;

public static class IdentityPermissionNames
{
    public const string ClaimType = "https://gabbium.dev/claims/permission";

    public const string CanListUsers = "identity:users.list";
    public const string CanGetUserById = "identity:users.get.byId";
    public const string CanCreateUser = "identity:users.create";
    public const string CanDeleteUser = "identity:users.delete";

    public const string CanListPermissions = "identity:permissions.list";
    public const string CanGetPermissionById = "identity:permissions.get.byId";
    public const string CanCreatePermission = "identity:permissions.create";
    public const string CanDeprecatePermission = "identity:permissions.deprecate";
    public const string CanDeletePermission = "identity:permissions.delete";

    public static IEnumerable<string> All
    {
        get
        {
            yield return CanListUsers;
            yield return CanGetUserById;
            yield return CanCreateUser;
            yield return CanDeleteUser;
            yield return CanListPermissions;
            yield return CanGetPermissionById;
            yield return CanCreatePermission;
            yield return CanDeprecatePermission;
            yield return CanDeletePermission;
        }
    }
}
