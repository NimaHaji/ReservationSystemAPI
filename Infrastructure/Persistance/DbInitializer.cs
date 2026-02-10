using Application.Interfaces.Repositories;
using Domain;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistance;

public class DbInitializer
{
    
    public static void Seed(AppDbContext context, IPasswordHasher passwordHasher,IConfiguration configuration)
    {
        if (!context.Users.Any(u => u.Role == UserRole.Admin))
        {
            var FullName=configuration["SeedAdmin:FullName"];
            var Email=configuration["SeedAdmin:Email"];
            var PhoneNumber=configuration["SeedAdmin:PhoneNumber"];
            var Password=passwordHasher.Hash(configuration["SeedAdmin:Password"]);
            var role=UserRole.Admin;
            
            var User=new User(FullName,Email,PhoneNumber,role,Password);
            
            context.Users.Add(User);
            context.SaveChanges();
        }
    }
}