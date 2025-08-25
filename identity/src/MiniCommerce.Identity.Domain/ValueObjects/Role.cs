namespace MiniCommerce.Identity.Domain.ValueObjects;

public sealed record Role(string Value)
{
    public static readonly Role User = new("User");
    public static readonly Role Administrator = new("Administrator");

    public static Role From(string value) => value.ToLowerInvariant() switch
    {
        "user" => User,
        "administrator" => Administrator,
        _ => throw new ArgumentOutOfRangeException(nameof(value), "Invalid role.")
    };

    public override string ToString() => Value;
}
