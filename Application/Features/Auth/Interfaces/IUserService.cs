using Application.Features.Auth.DTOs;

namespace Application.Features.Auth.Interfaces;

public interface IUserService
{
    Task<string> RegisterUserAsync(RegisterUser registerUser);
    Task<LoginResponse> LoginUserAsync(LoginUser loginUser);
    Task<string> LogoutUserAsync();
    Task<LoginResponse> RefreshTokenAsync(string refreshToken);
    Task<List<ViewUsers>> GetAllUsersAsync();
}