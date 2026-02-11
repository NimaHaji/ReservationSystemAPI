using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories;

public class AppointmentServiceLinkRepository:IAppointmenServiceLinkRepository
{
    private readonly AppDbContext _context;

    public AppointmentServiceLinkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> GetServiceIdsByAppointmentId(Guid appointmentId)
    {
        return await _context.AppointmentServiceLinks
            .Where(x => x.AppointmentId == appointmentId)
            .Select(x => x.ServiceId)
            .ToListAsync();
    }

    public async Task AddRangeAsync(Guid appointmentId, IEnumerable<Guid> serviceIds)
    {
        await _context.AppointmentServiceLinks.AddRangeAsync(
            serviceIds.Select(id =>
                new AppointmentServiceLink(appointmentId, id)));
         await SaveAsync();
    }

    public async Task RemoveRangeAsync(Guid appointmentId, IEnumerable<Guid> serviceIds)
    {
         _context.AppointmentServiceLinks.RemoveRange(
            serviceIds.Select(id =>
                new AppointmentServiceLink(appointmentId, id)));
         await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}