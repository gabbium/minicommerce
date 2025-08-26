using MiniCommerce.Identity.Domain.Aggregates.Users;

namespace MiniCommerce.Identity.Application.Abstractions;

public interface IJwtTokenService
{
    string CreateAccessToken(User user);
}
