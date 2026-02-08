using Application.DTO_s.User;

namespace Application.Interfaces;

public interface IUserService
{
    Task<string> RegisterUserAsync(RegisterUser registerUser);
    Task<LoginResponse> LoginUserAsync(LoginUser loginUser);
    Task<LoginResponse> RefreshTokenAsync(string refreshToken);
    Task<List<ViewUsers>> GetAllUsersAsync();
}