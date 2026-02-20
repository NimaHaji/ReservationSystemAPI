using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Features.Auth.DTOs;
using Application.Features.Auth.Interfaces;
using Domain.Entities;

namespace Application.Features.Auth.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUSerContext _userContext;
    private readonly IVerificationCodeGenerator _verificationCodeGenerator;

    public UserService(IUserRepository repository, IJwtTokenService jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository, IHasher hasher, IPasswordHasher passwordHasher,
        IUSerContext userContext, IVerificationCodeGenerator verificationCodeGenerator)
    {
        _userRepository = repository;
        _jwtTokenService = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _hasher = hasher;
        _passwordHasher = passwordHasher;
        _userContext = userContext;
        _verificationCodeGenerator = verificationCodeGenerator;
    }

    public async Task<string> RegisterUserAsync(RegisterUserRequestDto registerUserRequestDto)
    {
        var password = _passwordHasher.Hash(registerUserRequestDto.Password);
        var user = new User(registerUserRequestDto.FullName, registerUserRequestDto.Email,
            registerUserRequestDto.PhoneNumber, password);
        await _userRepository.RegisterUserAsync(user);
        return $"{user.FullName} Registred";
    }

    public async Task<LoginUserResponseDto> LoginUserAsync(LoginUserRequestDto loginUserRequestDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginUserRequestDto.Email);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password");

        var isValid = _passwordHasher.Verify(user.Password, loginUserRequestDto.Password);

        if (!isValid)
            throw new UnauthorizedAccessException("Invalid email or password");

        var token = _jwtTokenService.GenerateJwtToken(user);
        var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();
        var refreshTokenHash = _hasher.Hash(refreshTokenValue);
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshTokenHash,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.AddAsync(refreshToken);
        await _refreshTokenRepository.SaveChangesAsync();

        return new LoginUserResponseDto(token, refreshTokenValue);
    }

    public async Task<string> LogoutUserAsync()
    {
        var userId = _userContext.UserId;
        var refreshTokens = await _refreshTokenRepository.GetRefreshTokensByIdAsync(userId);
        foreach (var token in refreshTokens)
        {
            token.IsRevoked = true;
        }

        await _refreshTokenRepository.SaveChangesAsync();
        return "User Logged out .";
    }

    public async Task<LoginUserResponseDto> RefreshTokenAsync(string refreshToken)
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

        string? newRefreshTokenValue = null;

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
        return new LoginUserResponseDto(
            newAccessToken,
            newRefreshTokenValue ?? refreshToken
        );
    }

    public async Task<ProfileResponseDto> ViewProfileAsync()
    {
        var userId = _userContext.UserId;
        var user = await _userRepository.GetUserByIdAsync(userId);
        return new ProfileResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.ToString()
        };
    }

    public async Task<ProfileResponseDto> UpdateProfileAsync(UpdateProfileRequestDto dto)
    {
        var userId = _userContext.UserId;
        var user = await _userRepository.GetUserByIdAsync(userId)
            ??  throw new UnauthorizedAccessException("Invalid user id");
        
        user.UpdateProfile(dto.FullName,dto.PhoneNumber);
        
        await _userRepository.SaveChangesAsync();

        return new ProfileResponseDto()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.ToString()
        };
    }

    public async Task<List<ViewUser>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }
}