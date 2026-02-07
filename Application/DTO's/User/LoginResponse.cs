namespace Application.DTO_s.User;

public class LoginResponse
{
    public string AccessToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}