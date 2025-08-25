using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Domain.Entities;
using MiniCommerce.Identity.Infrastructure.Security;

namespace MiniCommerce.Identity.Infrastructure.Jwt;

public class JwtTokenService(IOptions<JwtOptions> jwtOptions) : IJwtTokenService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public string CreateAccessToken(User user)
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.Value)
        };

        var permissions = Permissions.All
            .Where(p => p.Roles.Contains(user.Role))
            .Select(p => p.Name);

        foreach (var permission in permissions)
            claims.Add(new("permission", permission));

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
