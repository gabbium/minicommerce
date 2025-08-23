using MiniCommerce.Identity.Application.Interfaces;
using MiniCommerce.Identity.Domain.Entities;

namespace MiniCommerce.Identity.Infrastructure.Services;

public class TokenService : ITokenService
{
    public string CreateAccessToken(User user)
    {
        return user.Id.ToString();
    }
}
