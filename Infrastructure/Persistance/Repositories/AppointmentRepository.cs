using Application.Features.Appointments.DTOs;
using Application.Features.Appointments.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories;

public class AppointmentRepository:IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _context
            .Appointments
            .AddAsync(appointment);
        await SaveAsync();
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

    public async Task<List<ViewAppointments>> ViewAppointments(Guid userId)
    {
        return await _context
            .Appointments
            .Include(x=>x.AppointmentServices)
            .ThenInclude(x=>x.Service)
            .Where(x => x.UserId == userId)
            .Select(x=>new ViewAppointments
            {
                AppointmentsTitle =x.Title,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status.ToString(),
                Services = x.AppointmentServices
                    .Select(s=>s.Service.Title)
                    .ToList()
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
