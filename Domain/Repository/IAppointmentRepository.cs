using Domain.Aggregates.Appointment;

namespace Domain.Repository;

public interface IAppointmentRepository
{
    Task AddAppointmentAsync(Appointment appointment);
    Task<List<Appointment>> ViewAppointments();
    Task<List<Appointment>> ViewAppointments(Guid userId);
    Task<Appointment?> GetAppointmentByIdAsync(Guid appointmentId);
    Task<bool> IsExistBy(Guid appointmentId);
}