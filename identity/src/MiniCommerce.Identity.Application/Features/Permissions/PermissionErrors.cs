namespace MiniCommerce.Identity.Application.Features.Permissions;

public static class PermissionErrors
{
    public static Error NotFound => Error.NotFound("Permissions.NotFound", "The specified permission was not found.");
}
