using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Features.Auth.DTOs;
using Application.Features.Auth.Interfaces;
using Domain.Entities;

namespace Application.Features.Auth.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IHasher _hasher;
    private readonly IPasswordHasher _passwordHasher; 
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public UserService(IUserRepository repository, IJwtTokenService jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository, IHasher hasher, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _jwtTokenService = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _hasher = hasher;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> RegisterUserAsync(RegisterUser registerUser)
    {
        var password = _passwordHasher.Hash(registerUser.Password);
        var user = new User(registerUser.FullName, registerUser.Email, registerUser.PhoneNumber, password);
        await _repository.RegisterUserAsync(user);
        return $"{user.FullName} Registred";
    }

    public async Task<LoginResponse> LoginUserAsync(LoginUser loginUser)
    {
        var user = await _repository.GetUserByEmailAsync(loginUser);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password");

        var isValid = _passwordHasher.Verify(user.Password, loginUser.Password);

        if (!isValid)
            throw new UnauthorizedAccessException("Invalid email or password");

        var token = _jwtTokenService.GenerateJwtToken(user);
        var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();
        var refreshTokenHash =_hasher.Hash(refreshTokenValue);
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshTokenHash,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.AddAsync(refreshToken);
        await _refreshTokenRepository.SaveChangesAsync();

        return new LoginResponse(token, refreshTokenValue);
    }

    public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
    {
        var hashedToken = _hasher.Hash(refreshToken);

        var storedToken = await _refreshTokenRepository.GetAsync(hashedToken)
                          ?? throw new Exception("Invalid refresh token");

        if (storedToken.IsRevoked)
            throw new Exception("Refresh token already used");

        if (storedToken.ExpiresAt <= DateTime.UtcNow)
            throw new Exception("Refresh token expired");

        var newAccessToken = _jwtTokenService.GenerateJwtToken(storedToken.User);
        
        var rotateWindow = TimeSpan.FromMinutes(5);

        string newRefreshTokenValue = null;

        if (storedToken.ExpiresAt - DateTime.UtcNow <= rotateWindow)
        {
            storedToken.IsRevoked = true;

            newRefreshTokenValue = _jwtTokenService.GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = _hasher.Hash(newRefreshTokenValue),
                UserId = storedToken.UserId,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });
        }

        await _refreshTokenRepository.SaveChangesAsync();
        return new LoginResponse(
            newAccessToken,
            newRefreshTokenValue ?? refreshToken
        );
    }



    public Task<List<ViewUsers>> GetAllUsersAsync()
    {
        return _repository.GetAllUsersAsync();
    }
}