using Application.DTO_s.Service;
using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class ServiceRepository:IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CreatServiceAsync(Service service)
    {
        await _context
            .Services
            .AddAsync(service);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<ViewServices>> ViewAllServiceAsync()
    {
        return await _context
            .Services
            .Select(x=>new ViewServices()
            {
                Title = x.Title,
                durationTime = x.DurationMinutes
            })
            .ToListAsync();
    }

    public async Task<Service?> GetServiceByIdAsync(Guid serviceId)
    {
        return await _context
            .Services
            .Where(x => x.Id == serviceId)
            .FirstOrDefaultAsync();
    }
}