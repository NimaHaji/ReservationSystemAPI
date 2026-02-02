using Application.Interfaces;
using Application.Interfaces.Repositories;

namespace Application.Services;

public class AppointmentService:IAppointmentService
{
    private IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }


    public async Task<Guid> CreateAppointmentAsync(CreateAppointment dto)
    {
        var apppointment = new Appointment(dto.UserID,dto.ServiceId,dto.StartTime,dto.EndTime);
        
        await _repository.AddAsync(apppointment);
        return apppointment.Id;
    }
}