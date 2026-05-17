using Domain.Base;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObject;

namespace Domain.Aggregates.User;

public class User:AggregatedRoot
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public UserRole Role { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Password Password { get; private set; }
    public string? PasswordResetCodeHash { get; private set; }
    public DateTime? PasswordResetCodeExpireAt { get; private set; }
    public int PasswordResetAttemptsCount { get; private set; }
    private readonly List<RefreshToken> _refreshTokens = new();
    public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens;

    public void AddRefreshToken(string refreshToken, DateTime expiresAt)
    {
        _refreshTokens.RemoveAll(tk => tk.IsExpired);
        _refreshTokens.Add(new RefreshToken(refreshToken, expiresAt));
    }

    public void RevokeRefreshToken(Guid tokenId)
    {
        var token = _refreshTokens.FirstOrDefault(tk => tk.Id == tokenId);
        if (token == null) throw new InvalidOperationException("Refresh token not found");
        token.Revoke();
    }

    private User()
    {
    }

    public User(string fullName, Email email, PhoneNumber phoneNumber, Password password)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Role = UserRole.User;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public User(string fullName, Email email, PhoneNumber phoneNumber, UserRole role, Password password)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Role = role;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }

    public void UpdateProfile(string fullName, PhoneNumber phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Full name required");
        if (phoneNumber == null) throw new ArgumentException("Phone number required");
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public void ResetPassword(string codeHash, DateTime expiresAt)
    {
        if (string.IsNullOrWhiteSpace(codeHash)) throw new ArgumentException("Code hash required");
        if (expiresAt <= DateTime.UtcNow) throw new ArgumentException("Expires must be in future");
        PasswordResetCodeHash = codeHash;
        PasswordResetCodeExpireAt = expiresAt;
        PasswordResetAttemptsCount = 0;
    }

    private const int MaxResetAttempts = 5;

    public bool CanUseResetPassword(string codeHash, DateTime now)
    {
        if (PasswordResetCodeExpireAt == null || now > PasswordResetCodeExpireAt)
            return false;
        if (PasswordResetAttemptsCount >= MaxResetAttempts)
            return false;
        return PasswordResetCodeHash == codeHash;
    }

    public void IncreasePasswordResetAttemptCount() => PasswordResetAttemptsCount++;

    public void ClearPasswordResetCode()
    {
        PasswordResetCodeHash = null;
        PasswordResetCodeExpireAt = null;
        PasswordResetAttemptsCount = 0;
    }
}