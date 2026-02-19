using Application.Features.Auth.DTOs;
using Domain.Entities;

namespace Application.Features.Auth.Interfaces;

public interface IUserRepository
{
    Task RegisterUserAsync(User user);
    Task<User?> GetUserByEmailAsync(LoginUserRequestDto loginUserRequestDto);
    Task<bool> IsUserExistsByIdAsync(Guid userId);
    Task SaveChangesAsync();
    Task<List<ViewUser>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid userId);
}