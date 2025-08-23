namespace MiniCommerce.Identity.Domain.Entities;

public sealed class User(string email) : BaseEntity
{
    public string Email { get; private set; } = email;
}
