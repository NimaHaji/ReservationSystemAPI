namespace Application.Interfaces;

public interface IAppointmentService
{
    Task<Guid> CreateAppointmentAsync(CreateAppointment dto);
}