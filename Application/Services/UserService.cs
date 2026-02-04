using Application.DTO_s.User;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain;

namespace Application.Services;

public class UserService:IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasherService;
    private readonly IJwtService _jwtService;
    public UserService(IUserRepository repository, IPasswordHasher passwordHasherService, IJwtService jwtService)
    {
        _repository = repository;
        _passwordHasherService = passwordHasherService;
        _jwtService = jwtService;
    }

    public async Task<string> RegisterUserAsync(RegisterUser registerUser)
    {
        var password= _passwordHasherService.Hash(registerUser.Password);
        var user=new User(registerUser.FullName,registerUser.Role,registerUser.Email,registerUser.PhoneNumber,password);
        
        await _repository.RegisterUserAsync(user);
        return $"{user.FullName} Registred";
    }

    public async Task<string> LoginUserAsync(LoginUser loginUser)
    {
        var user=await _repository.GetUserByEmailAsync(loginUser);
        
        var isvalid =_passwordHasherService.Verify(user.Password,loginUser.Password);
        
        //JWT
        _jwtService.GenerateJwtToken(user);
        
        if(isvalid) return "login successful";
        else return "password doesn't match";
    }
}