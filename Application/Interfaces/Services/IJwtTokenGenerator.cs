using Domain;

namespace Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
}