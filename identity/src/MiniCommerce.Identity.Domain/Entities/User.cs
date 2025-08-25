using MiniCommerce.Identity.Domain.ValueObjects;

namespace MiniCommerce.Identity.Domain.Entities;

public sealed class User(string email, Role role)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; private set; } = email;
    public Role Role { get; private set; } = role;
}
