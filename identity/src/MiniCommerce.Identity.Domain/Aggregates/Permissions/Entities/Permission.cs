namespace MiniCommerce.Identity.Domain.Aggregates.Permissions.Entities;

public sealed class Permission(string code) : IAggregateRoot
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Code { get; private set; } = code;
    public bool Deprecated { get; private set; }

    public void Deprecate() => Deprecated = true;
}
