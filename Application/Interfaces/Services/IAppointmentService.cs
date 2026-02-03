namespace Application.Interfaces;

public interface IAppointmentService
{
    Task<Guid> CreateAppointmentAsync(CreateAppointment dto);
    Task<string> DeleteAppointmentAsync(Guid id);
    Task<Appointment?> GetAppointmentByIdAsync(Guid appointmentId);
    Task<string> UpdateAppointmentAsync(Guid AppointmentId,EditAppointment editAppointment);
    Task<List<ViewAppointments>> ViewAppointments();
    Task<bool> IsExistBy(Guid appointmentId);
}