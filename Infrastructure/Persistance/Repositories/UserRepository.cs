using Application.DTO_s.User;
using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task RegisterUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAsync(LoginUser loginUser)
    {
        return await _context
            .Users
            .Where(x => loginUser.Email == x.Email)
            .FirstOrDefaultAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<ViewUsers>> GetAllUsersAsync()
    {
        return await _context
            .Users
            .Select(x => new ViewUsers
            {
                FullName = x.FullName,
                Email = x.Email,
                UserRole = x.Role.ToString(),
                PhoneNumber = x.PhoneNumber,
            }).ToListAsync();
    }
}