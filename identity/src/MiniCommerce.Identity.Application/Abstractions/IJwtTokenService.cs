using MiniCommerce.Identity.Domain.Entities;

namespace MiniCommerce.Identity.Application.Abstractions;

public interface IJwtTokenService
{
    string CreateAccessToken(User user);
}
