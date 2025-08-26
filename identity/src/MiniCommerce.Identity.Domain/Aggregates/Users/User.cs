namespace MiniCommerce.Identity.Domain.Aggregates.Users;

public sealed class User(string email)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; private set; } = email;
}
