namespace Application.DTO_s.User;

public record LoginResponse(
    string AccessToken,
    string RefreshToken);

public record RefreshTokenRequest (string RefreshToken);
