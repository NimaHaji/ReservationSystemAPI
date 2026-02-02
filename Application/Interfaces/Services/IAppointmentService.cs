namespace Application.Interfaces;

public interface IAppointmentService
{
    Task<Guid> CreateAppointmentAsync(CreateAppointment dto);
    Task<Appointment> GetAppointmentByIDAsync(Guid appointmentId);
    Task<bool> IsExistBy(Guid id);
    Task<string> DeleteAppointmentAsync(Guid id);
}