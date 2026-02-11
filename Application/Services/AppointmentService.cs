using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain;

namespace Application.Services;

public class AppointmentService:IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IAppointmenServiceLinkRepository _appointmenServiceLinkRepository;
    
    public AppointmentService(IAppointmentRepository repository, IUserRepository userRepository, IServiceRepository serviceRepository, IAppointmenServiceLinkRepository appointmenServiceLinkRepository)
    {
        _appointmentRepository = repository;
        _userRepository = userRepository;
        _serviceRepository = serviceRepository;
        _appointmenServiceLinkRepository = appointmenServiceLinkRepository;
    }


     public async Task<string> CreateAppointmentAsync(CreateAppointment dto)
    {
        var appointment=new Appointment(dto.UserID,dto.StartTime, dto.EndTime,dto.AppoinmentTitle);

        if (!await _userRepository.IsUserExistsByIdAsync(dto.UserID))
            return "User not found";

        var services = await _serviceRepository
            .GetServiceListByIdsAsync(dto.ServiceId);
        
        if (services.Count == 0)
            return "Services Is Empty";
        
        if (services.Count != dto.ServiceId.Count)
            return "One or more services not found";
        
        appointment.AddServices(services);
        
        await _appointmentRepository.AddAsync(appointment);
        await _appointmentRepository.SaveAsync();
        return $"{appointment.Title} Created";
    }

    public async Task<string> UpdateAppointmentAsync(Guid appointmentId,EditAppointment dto)
    {
        var currentServiceIds =
            await _appointmenServiceLinkRepository
                .GetServiceIdsByAppointmentId(appointmentId);

        var toAdd = dto.ServiceIds.Except(currentServiceIds);
        var toRemove = currentServiceIds.Except(dto.ServiceIds);

        await _appointmenServiceLinkRepository
            .RemoveRangeAsync(appointmentId, toRemove);

        await _appointmenServiceLinkRepository
            .AddRangeAsync(appointmentId, toAdd);
        return "Appointment Updated";
    }

    public async Task<List<ViewAppointments>> ViewAppointments()
    {
        return await _appointmentRepository.ViewAppointments();
    }

    public async Task<List<ViewAppointments>> ViewAppointments(Guid userId)
    {
        return await _appointmentRepository.ViewAppointments(userId);
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(Guid appointmentId)
    {
        return await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
    }

    public async Task<bool> IsExistBy(Guid appointmentId)
    {
        return await _appointmentRepository.IsExistBy(appointmentId);
    }

    public async Task<string> DeleteAppointmentAsync(Guid id)
    {
        var appoinment = await GetAppointmentByIdAsync(id);
        appoinment?.Cancel();
        await _appointmentRepository.SaveAsync();
        return appoinment?.Status == AppointmentStatus.Cancelled ? $"{appoinment.Title} Canceled" : "Not Cancled";
    }
}