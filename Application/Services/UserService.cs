
using Application.DTO_s.User;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain;

namespace Application.Services;

public class UserService:IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasherService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public UserService(IUserRepository repository, IPasswordHasher passwordHasherService, IJwtTokenGenerator jwtTokenGenerator)
    {
        _repository = repository;
        _passwordHasherService = passwordHasherService;
        _jwtTokenGenerator = jwtTokenGenerator;
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
        var user = await _repository.GetUserByEmailAsync(loginUser);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password");

        var isValid = _passwordHasherService.Verify(user.Password, loginUser.Password);

        if (!isValid)
            throw new UnauthorizedAccessException("Invalid email or password");
        
        var token= _jwtTokenGenerator.GenerateJwtToken(user);
        return token;
    }


    public Task<List<ViewUsers>> GetAllUsersAsync()
    {
        return _repository.GetAllUsersAsync();
    }
}