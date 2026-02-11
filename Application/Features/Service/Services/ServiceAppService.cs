using Application.Features.Service.DTOs;
using Application.Features.Service.Interfaces;

namespace Application.Features.Service.Services;

public class ServiceAppService:IServiceAppService
{
    private readonly IServiceRepository _repository;

    public ServiceAppService(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> CreateServiceAsync(CreateService createService)
    {
        var service=new Domain.Entities.Service(createService.Title,createService.DurationMinutes);
        await _repository.CreatServiceAsync(service);
        return $"{service.Title} created";
    }
    
    public async Task<string> EditServiceAsync(Guid serviceId,EditService editService)
    {
        var appointment = await _repository.GetServiceByIdAsync(serviceId) ?? throw new Exception("Appointment Not Found");
        
        appointment.Edit(editService.DurationMinutes,editService.Title);
        await _repository.SaveChangesAsync();
        return $"{appointment.Title} Updated";
    }

    public async Task<List<ViewServices>> ViewAllServicesAsync()
    {
        return await _repository.ViewAllServiceAsync();
    }

}