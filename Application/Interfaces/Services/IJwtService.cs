using Domain;

namespace Application.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}