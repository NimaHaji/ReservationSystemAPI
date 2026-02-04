using Application.DTO_s.User;
using Domain;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task RegisterUserAsync(User user);
    Task<User?> GetUserByEmailAsync(LoginUser loginUser);
    Task SaveChangesAsync();
}