using Application.Features.Appointments.DTOs;
using Domain.Entities;

namespace Application.Features.Appointments.Interfaces;

public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment);
    Task SaveAsync();
    Task<List<ViewAppointments>> ViewAppointments();
    Task<List<ViewAppointments>> ViewAppointments(Guid userId);
    Task<Appointment?> GetAppointmentByIdAsync(Guid appointmentId);
    Task<bool> IsExistBy(Guid appointmentId);
}