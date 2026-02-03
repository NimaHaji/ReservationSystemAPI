using Application;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class AppointmentRepository:IAppointmentRepository
{
    private readonly AppDbContext _context;
    private IAppointmentRepository _appointmentRepositoryImplementation;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        SaveAsync();
    }

    public async Task<List<ViewAppointments>> ViewAppointments()
    {
        return await _context
            .Appointments
            .Select(x => new ViewAppointments
            {
                AppointmentsTitle = x.Title,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status.ToString()
            })
            .ToListAsync();
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(Guid appointmentId)
    {
         return await _context.Appointments.FirstOrDefaultAsync(x=>x.AppointmentId == appointmentId);
    }

    public async Task<string?> GetTitleByIdAsync(Guid appointmentId)
    {
        return await _context
            .Appointments
            .Where(x => x.AppointmentId == appointmentId)
            .Select(x => x.Title)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistBy(Guid appointmentId)
    {
        return await _context.Appointments.AnyAsync(x=>x.AppointmentId == appointmentId);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
