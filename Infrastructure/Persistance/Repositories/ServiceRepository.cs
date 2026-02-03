using Application.DTO_s.Service;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain;

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
}