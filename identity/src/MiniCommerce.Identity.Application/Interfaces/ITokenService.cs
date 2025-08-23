using MiniCommerce.Identity.Domain.Entities;

namespace MiniCommerce.Identity.Application.Interfaces;

public interface ITokenService
{
    string CreateAccessToken(User user);
}
