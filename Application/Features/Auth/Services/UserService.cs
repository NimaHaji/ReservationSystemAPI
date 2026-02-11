using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Features.Auth.DTOs;
using Application.Features.Auth.Interfaces;
using Domain.Entities;

namespace Application.Features.Auth.Services;

public class UserService:IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasherService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    public UserService(IUserRepository repository, IPasswordHasher passwordHasherService, IJwtTokenService jwtTokenGenerator, IRefreshTokenRepository refreshTokenRepository)
    {
        _repository = repository;
        _passwordHasherService = passwordHasherService;
        _jwtTokenService = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<string> RegisterUserAsync(RegisterUser registerUser)
    {
        var password= _passwordHasherService.Hash(registerUser.Password);
        var user=new User(registerUser.FullName,registerUser.Email,registerUser.PhoneNumber,password);
        await _repository.RegisterUserAsync(user);
        return $"{user.FullName} Registred";
    }

    public async Task<LoginResponse> LoginUserAsync(LoginUser loginUser)
    {
        var user = await _repository.GetUserByEmailAsync(loginUser);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password");

        var isValid = _passwordHasherService.Verify(user.Password, loginUser.Password);

        if (!isValid)
            throw new UnauthorizedAccessException("Invalid email or password");
        
        var token= _jwtTokenService.GenerateJwtToken(user);
        var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshTokenValue,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
        
        await _refreshTokenRepository.AddAsync(refreshToken);
        await _refreshTokenRepository.SaveChangesAsync();

        return new LoginResponse(token, refreshTokenValue);
    }

    public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetAsync(refreshToken)
                          ?? throw new Exception("Invalid refresh token");

        if (storedToken.IsRevoked)
            throw new Exception("Refresh token already used");

        if (storedToken.ExpiresAt < DateTime.UtcNow)
            throw new Exception("Refresh token expired");

        storedToken.IsRevoked = true;

        var newAccessToken =
            _jwtTokenService.GenerateJwtToken(storedToken.User);

        var newRefreshTokenValue =
            _jwtTokenService.GenerateRefreshToken();

        await _refreshTokenRepository.AddAsync(new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = newRefreshTokenValue,
            UserId = storedToken.UserId,
            ExpiresAt = storedToken.ExpiresAt
        });

        await _refreshTokenRepository.SaveChangesAsync();

        return new LoginResponse(
            newAccessToken,
            newRefreshTokenValue);
    }


    public Task<List<ViewUsers>> GetAllUsersAsync()
    {
        return _repository.GetAllUsersAsync();
    }
}