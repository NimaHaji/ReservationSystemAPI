using Application.Features.Appointments.DTOs;
using Domain.Entities;

namespace Application.Features.Appointments.Interfaces;

public interface IAppointmentService
{
    Task<string> CreateAppointmentAsync(CreateAppointment dto);
    Task<string> CancelAppointmentAsync(Guid id);
    Task<Appointment?> GetAppointmentByIdAsync(Guid appointmentId);
    Task<string> UpdateAppointmentAsync(Guid appointmentId,EditAppointment editAppointment);
    Task<List<ViewAppointments>> ViewAppointments();
    Task<List<ViewAppointments>> ViewMyAppointments();
    Task<bool> IsExistBy(Guid appointmentId);
}