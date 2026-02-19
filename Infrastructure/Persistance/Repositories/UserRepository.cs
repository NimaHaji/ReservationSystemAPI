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

    public async Task<User?> GetUserByEmailAsync(LoginUserRequestDto loginUserRequestDto)
    {
        return await _context
            .Users
            .Where(x => loginUserRequestDto.Email == x.Email)
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

    public async Task<List<ViewUser>> GetAllUsersAsync()
    {
        return await _context
            .Users
            .Select(x => new ViewUser
            {
                FullName = x.FullName,
                Email = x.Email,
                UserRole = x.Role.ToString(),
                PhoneNumber = x.PhoneNumber,
            }).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _context
            .Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();
    }
}