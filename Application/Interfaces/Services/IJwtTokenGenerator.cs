using Domain;

namespace Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(User user);
}