using MiniCommerce.Identity.Infrastructure.Jwt;
using MiniCommerce.Identity.Web.Endpoints;

namespace MiniCommerce.Identity.Web.Extensions;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection("Jwt"))
            .ValidateOnStart();

        services.AddSingleton<IValidateOptions<JwtOptions>, JwtOptionsValidator>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                var jwt = configuration.GetSection("Jwt").Get<JwtOptions>()!;

                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization(o =>
        {
            foreach (var permissionName in PermissionNames.All)
            {
                o.AddPolicy(permissionName, p =>
                    p.RequireClaim(PermissionNames.ClaimType, permissionName));
            }
        });

        return services;
    }
}

