namespace Application.Interfaces.Repositories;

public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment);
}