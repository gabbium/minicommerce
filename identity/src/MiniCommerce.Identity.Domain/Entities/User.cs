namespace MiniCommerce.Identity.Domain.Entities;

public sealed class User(string email)
{
    public Guid Id { get; set; }
    public string Email { get; private set; } = email;
}
