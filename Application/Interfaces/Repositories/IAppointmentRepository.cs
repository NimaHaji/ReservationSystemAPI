namespace Application.Interfaces.Repositories;

public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment);
    Task RemoveAsync(Appointment appointment);
    Task<Appointment?> GetAppointmentBy(Guid appointmentId);
    Task<bool> IsExistBy(Guid appointmentId);
    Task SaveAsync();
}