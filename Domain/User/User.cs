namespace Domain;

public class User
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public UserRole Role { get; private set; }
    public string Email  { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Password { get;private set; }
    public ICollection<RefreshToken> RefreshTokens { get; private set; }=new List<RefreshToken>();
    public ICollection<Appointment> Appointments { get; private set; }=new List<Appointment>();

    public User()
    {
        
    }
    public User(string fullName, string email, string phoneNumber, string password)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Role = UserRole.User;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }
    public User(string fullName, string email, string phoneNumber,UserRole role, string password)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Role = role;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }
}

public enum UserRole
{
    Admin,
    User
}