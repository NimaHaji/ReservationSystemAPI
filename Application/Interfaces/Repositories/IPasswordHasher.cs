namespace Application.Interfaces.Repositories;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string hashedPassword, string password);
}