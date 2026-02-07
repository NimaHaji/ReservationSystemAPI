namespace Application.DTO_s.User;

public class UserToken
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}