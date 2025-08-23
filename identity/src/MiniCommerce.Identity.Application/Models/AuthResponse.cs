namespace MiniCommerce.Identity.Application.Models;

public record AuthResponse(UserResponse User, TokenResponse Token);
