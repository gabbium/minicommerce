namespace MiniCommerce.Identity.Domain.Aggregates.Permissions.Errors;

public static class PermissionErrors
{
    public static Error NotFound => Error.NotFound("Permissions.NotFound", "The specified permission was not found.");
}
