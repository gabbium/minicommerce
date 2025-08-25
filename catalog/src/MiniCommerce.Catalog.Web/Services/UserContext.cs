using MiniCommerce.Catalog.Application.Abstractions;

namespace MiniCommerce.Catalog.Web.Services;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new InvalidOperationException("User id is unavailable");
    }

    public static string GetEmail(this ClaimsPrincipal? principal)
    {
        string? email = principal?.FindFirstValue(ClaimTypes.Email);

        return !string.IsNullOrWhiteSpace(email) ?
            email :
            throw new InvalidOperationException("Email is unavailable");
    }
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new InvalidOperationException("User context is unavailable");

    public string Email =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetEmail() ??
        throw new InvalidOperationException("User context is unavailable");
}
