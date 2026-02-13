namespace Application.Features.Auth.DTOs;

public record LoginResponse(
    string AccessToken,
    string? RefreshToken);

public record RefreshTokenRequest (string RefreshToken);
