using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain;
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
        var apppointment = new Appointment(dto.UserID,dto.ServiceId,dto.StartTime,dto.EndTime,dto.AppoinmentTitle);
        
        await _repository.AddAsync(apppointment);
        return apppointment.Id;
    }

    public async Task<Appointment?> GetAppointmentByIDAsync(Guid id)
    {
        return await _repository.GetAppointmentBy(id);
    }

    public async Task<bool> IsExistBy(Guid appointmentId)
    {
        return await _repository.IsExistBy(appointmentId);
    }

    public async Task<string> DeleteAppointmentAsync(Guid id)
    {
        var appoinment = await GetAppointmentByIDAsync(id);
        appoinment.Cancel();
        
        await _repository.SaveAsync();
        return appoinment.Status == AppointmentStatus.Cancelled ? "Canceled" : "OK";
    }
}