using MiniCommerce.Catalog.Web.Endpoints;

namespace MiniCommerce.Catalog.Web.Extensions.Auth;

public static class AuthExtensions
{
    public static IServiceCollection AddAuthWithJwt(this IServiceCollection services, IConfiguration configuration)
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
            foreach (var permissionName in Permissions.All)
            {
                o.AddPolicy(permissionName, p =>
                    p.RequireClaim(Permissions.ClaimType, permissionName));
            }
        });

        return services;
    }
}

