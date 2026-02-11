using Application.Features.Auth.DTOs;
using Domain.Entities;

namespace Application.Features.Auth.Interfaces;

public interface IUserRepository
{
    Task RegisterUserAsync(User user);
    Task<User?> GetUserByEmailAsync(LoginUser loginUser);
    Task<bool> IsUserExistsByIdAsync(Guid userId);
    Task SaveChangesAsync();
    Task<List<ViewUsers>> GetAllUsersAsync();
    Task<List<Domain.Entities.Service>> GetByIdsAsync(List<Guid> ids);
}