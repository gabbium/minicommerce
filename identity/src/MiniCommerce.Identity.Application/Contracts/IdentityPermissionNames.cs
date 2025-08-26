namespace MiniCommerce.Identity.Application.Contracts;

public static class IdentityPermissionNames
{
    public const string ClaimType = "https://gabbium.dev/claims/permission";

    public const string CanCreateUser = "identity:users.create";
    public const string CanCreatePermission = "identity:permissions.create";

    public static IEnumerable<string> All
    {
        get
        {
            yield return CanCreateUser;
            yield return CanCreatePermission;
        }
    }
}
