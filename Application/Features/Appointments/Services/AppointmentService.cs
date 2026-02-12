using Application.Common.Interfaces;
using Application.Features.Appointments.DTOs;
using Application.Features.Appointments.Interfaces;
using Application.Features.AppointmentServiceLink.Interfaces;
using Application.Features.Auth.Interfaces;
using Application.Features.Service.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.Features.Appointments.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IAppointmenServiceLinkRepository _appointmenServiceLinkRepository;
    private readonly IUSerContext _userContext;

    public AppointmentService(IAppointmentRepository repository, IUserRepository userRepository,
        IServiceRepository serviceRepository, IAppointmenServiceLinkRepository appointmenServiceLinkRepository,
        IUSerContext userContext)
    {
        _appointmentRepository = repository;
        _userRepository = userRepository;
        _serviceRepository = serviceRepository;
        _appointmenServiceLinkRepository = appointmenServiceLinkRepository;
        _userContext = userContext;
    }


    public async Task<string> CreateAppointmentAsync(CreateAppointment dto)
    {
        if (!_userContext.IsAuthenticated)
            throw new UnauthorizedAccessException("Not Authenticated");

        var userId = _userContext.UserId;
        var appointment = new Appointment(userId, dto.StartTime, dto.EndTime, dto.AppoinmentTitle);

        if (!await _userRepository.IsUserExistsByIdAsync(userId))
            throw new NotFoundException($"User with {userId} does not exist");

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

    public async Task<string> UpdateAppointmentAsync(Guid appointmentId, EditAppointment dto)
    {
        if (!_userContext.IsAuthenticated)
            throw new UnauthorizedAccessException("Not Authenticated");
        
        var userId = _userContext.UserId;
        
        var appointment= await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
        
        if (appointment == null)
            throw new NotFoundException($"Appointment with id {appointmentId} does not exist");
        
        if (appointment.UserId != userId)
            throw new ForbiddenAccessException("You are not allowed to edit this appointment");
        
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
        if (_userContext.Role != UserRole.Admin)
            throw new ForbiddenAccessException("Only admin users can view appointments");
        
        return await _appointmentRepository.ViewAppointments();
    }

    public async Task<List<ViewAppointments>> ViewMyAppointments()
    {
        if (!_userContext.IsAuthenticated)
            throw new UnauthorizedAccessException("Not Authenticated");
        return await _appointmentRepository.ViewAppointments(_userContext.UserId);
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(Guid id)
    {
        if(!_userContext.IsAuthenticated)
            throw new UnauthorizedAccessException("Not Authenticated");
        return await _appointmentRepository.GetAppointmentByIdAsync(_userContext.UserId);
    }

    public async Task<bool> IsExistBy(Guid appointmentId)
    {
        return await _appointmentRepository.IsExistBy(appointmentId);
    }

    public async Task<string> CancelAppointmentAsync(Guid id)
    {
        if (!_userContext.IsAuthenticated)
            throw new UnauthorizedAccessException("Not Authenticated");

        var appoinment = await GetAppointmentByIdAsync(id);
        if (appoinment?.UserId != _userContext.UserId)
            throw new ForbiddenAccessException("Not Accessible");

        if (appoinment is null)
            throw new NotFoundException($"Appointment does not exist");
        
        appoinment.Cancel();
        await _appointmentRepository.SaveAsync();
        return appoinment.Status == AppointmentStatus.Cancelled ? $"{appoinment.Title} Canceled" : "Not Cancled";
    }
}