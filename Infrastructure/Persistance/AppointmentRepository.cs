using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class AppointmentRepository:IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task<Appointment?> GetAppointmentBy(Guid appointmentId)
    {
         return await _context.Appointments.FirstOrDefaultAsync(x=>x.Id == appointmentId);
    }

    public async Task<bool> IsExistBy(Guid appointmentId)
    {
        return await _context.Appointments.AnyAsync(x=>x.Id == appointmentId);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
