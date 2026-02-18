using Application.Common.Interfaces;
using Application.Features.Service.DTOs;
using Application.Features.Service.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Entities;

namespace Application.Features.Service.Services;

public class ServiceAppService:IServiceAppService
{
    private readonly IServiceRepository _repository;
    private readonly IUSerContext _userContext;
    public ServiceAppService(IServiceRepository repository, IUSerContext context)
    {
        _repository = repository;
        _userContext = context;
    }
    
    private void EnsureAdmin()
    {
        if (_userContext.Role != UserRole.Admin)
            throw new ForbiddenAccessException("Only Admin Access");
    }
    public async Task<string> CreateServiceAsync(CreateService createService)
    {
        EnsureAdmin();
        var service=new Domain.Entities.Service(createService.Title,createService.DurationMinutes);
        await _repository.CreatServiceAsync(service);
        return $"{service.Title} created";
    }
    
    public async Task<string> EditServiceAsync(Guid serviceId,EditService editService)
    {
        EnsureAdmin();
        var appointment = await _repository.GetServiceByIdAsync(serviceId) ?? throw new NotFoundException("Service not found");
        
        appointment.Edit(editService.DurationMinutes,editService.Title);
        await _repository.SaveChangesAsync();
        return $"{appointment.Title} Updated";
    }

    public async Task<string> DeleteServiceAsync(Guid serviceId)
    {
        EnsureAdmin();
        await _repository.DeleteServiceAsync(serviceId);
        return $"{serviceId} deleted";
    }

    public async Task<List<ViewServices>> ViewAllServicesAsync()
    {
        return await _repository.ViewAllServiceAsync();
    }

}