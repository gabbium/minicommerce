namespace MiniCommerce.Identity.Application.Contracts;

public static class IdentityPermissionNames
{
    public const string ClaimType = "https://gabbium.dev/claims/permission";

    public const string CanListUsers = "identity:users.list";
    public const string CanGetUserById = "identity:users.get.byId";
    public const string CanCreateUser = "identity:users.create";
    public const string CanUpdateUser = "identity:users.update";
    public const string CanDeleteUser = "identity:users.delete";

    public const string CanCreatePermission = "identity:permissions.create";

    public static IEnumerable<string> All
    {
        get
        {
            yield return CanListUsers;
            yield return CanGetUserById;
            yield return CanCreateUser;
            yield return CanUpdateUser;
            yield return CanDeleteUser;
            yield return CanCreatePermission;
        }
    }
}
