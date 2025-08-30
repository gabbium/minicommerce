using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;

namespace MiniCommerce.Identity.Application.Abstractions;

public interface IJwtTokenService
{
    string CreateAccessToken(User user);
}
