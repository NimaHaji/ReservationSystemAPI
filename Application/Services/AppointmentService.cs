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
        return apppointment.AppointmentId;
    }

    public async Task<string> UpdateAppointmentAsync(Guid AppointmentId,EditAppointment editComponent)
    {
        var appointment = await _repository.GetAppointmentByIdAsync(AppointmentId) ?? throw new Exception("Appointment Not Found");
        
        appointment.Edit(editComponent.StartTime,editComponent.EndTime);
        await _repository.SaveAsync();
        return $"{appointment.Title} Updated";
    }

    public async Task<List<ViewAppointments>> ViewAppointments()
    {
        return await _repository.ViewAppointments();
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(Guid appointmentId)
    {
        return await _repository.GetAppointmentByIdAsync(appointmentId);
    }

    public async Task<bool> IsExistBy(Guid appointmentId)
    {
        return await _repository.IsExistBy(appointmentId);
    }

    public async Task<string> DeleteAppointmentAsync(Guid id)
    {
        var appoinment = await GetAppointmentByIdAsync(id);
        appoinment.Cancel();
        
        await _repository.SaveAsync();
        return appoinment.Status == AppointmentStatus.Cancelled ? $"{appoinment.Title} Canceled" : "Not Cancled";
    }
}