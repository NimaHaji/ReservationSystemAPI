using Application.DTO_s.User;

namespace Application.Interfaces;

public interface IUserService
{
    Task<string> RegisterUserAsync(RegisterUser registerUser);
    Task<string> LoginUserAsync(LoginUser loginUser);
    Task<List<ViewUsers>> GetAllUsersAsync();
}