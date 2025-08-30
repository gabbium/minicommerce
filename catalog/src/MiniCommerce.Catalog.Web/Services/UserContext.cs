using MiniCommerce.Catalog.Application.Abstractions;
using MiniCommerce.Catalog.Web.Extensions;

namespace MiniCommerce.Catalog.Web.Services;

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
