using Domain.Aggregates.User;

namespace Domain.Repository;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> IsUserExistsByIdAsync(Guid userId);
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid userId);
}