namespace MiniCommerce.Identity.Application.Contracts.Permissions;

public record PermissionResponse(Guid Id, string Code, bool Deprecated);
