using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Web.Extensions;

namespace MiniCommerce.Identity.Web.Services;

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
