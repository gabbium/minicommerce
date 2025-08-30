using MiniCommerce.Catalog.Infrastructure.Jwt;
using MiniCommerce.Catalog.Web.Endpoints;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Auth;

public sealed class JwtTokenFactory(IOptions<JwtOptions> jwtOptions)
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public string CreateAccessToken(string email, params string[] permissions)
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, email)
        };

        claims.AddRange(permissions.Select(p => new Claim(Policies.ClaimType, p)));

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
