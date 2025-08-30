namespace MiniCommerce.Catalog.Web.Extensions.Auth;

public class JwtOptions
{
    public string Secret { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
}

public class JwtOptionsValidator : IValidateOptions<JwtOptions>
{
    public ValidateOptionsResult Validate(string? name, JwtOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Secret))
            return ValidateOptionsResult.Fail("JWT secret not configured");

        if (string.IsNullOrWhiteSpace(options.Issuer))
            return ValidateOptionsResult.Fail("JWT issuer not configured");

        if (string.IsNullOrWhiteSpace(options.Audience))
            return ValidateOptionsResult.Fail("JWT audience not configured");

        return ValidateOptionsResult.Success;
    }
}
