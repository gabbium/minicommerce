namespace MiniCommerce.Identity.Application.Features.Users;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound("Users.NotFound", "The specified user was not found.");
}
