namespace Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }

    public string Token { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public bool IsExpired => ExpiresAt < DateTime.UtcNow;
    public bool IsActive => !IsExpired && !IsRevoked;

    internal RefreshToken(string token, DateTime expiresAt)
    {
        Id = Guid.NewGuid();
        Token = token ?? throw new ArgumentNullException(nameof(token));
        ExpiresAt = expiresAt;
        IsRevoked = false;
    }
    public void Revoke()=> IsRevoked = true;
}