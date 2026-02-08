using Domain;

namespace Infrastructure;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetAsync(string token);
    Task AddAsync(RefreshToken token);
    Task SaveChangesAsync();
}