using Domain;

namespace Application.DTO_s.User;

public class RegisterUser
{
    public string FullName { get; set; }
    public UserRole Role { get; set; }
    public string Email  { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}