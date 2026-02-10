using Application.DTO_s.User;
using Domain;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task RegisterUserAsync(User user);
    Task<User?> GetUserByEmailAsync(LoginUser loginUser);
    Task<bool> IsUserExistsByIdAsync(Guid userId);
    Task SaveChangesAsync();
    Task<List<ViewUsers>> GetAllUsersAsync();
    Task<List<Service>> GetByIdsAsync(List<Guid> ids);
}