using Application.Features.Auth.DTOs;
using Application.Features.Auth.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories;

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

    public async Task<bool> IsUserExistsByIdAsync(Guid userId)
    {
        return await _context.Users.AnyAsync(x => x.Id == userId);
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

    public async Task<List<Service>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.Services
            .Where(s => ids.Contains(s.Id))
            .ToListAsync();
    }
}