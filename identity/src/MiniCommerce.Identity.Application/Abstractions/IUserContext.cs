namespace MiniCommerce.Identity.Application.Abstractions;

public interface IUserContext
{
    Guid UserId { get; }
    string Email { get; }
}
