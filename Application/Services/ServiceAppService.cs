using Application.DTO_s.Service;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain;

namespace Application.Services;

public class ServiceAppService:IServiceAppService
{
    private readonly IServiceRepository _Repository;

    public ServiceAppService(IServiceRepository repository)
    {
        _Repository = repository;
    }

    public async Task<string> CreateServiceAsync(CreateService createService)
    {
        var service=new Service(createService.Title,createService.DurationMinutes);
        
        await _Repository.CreatServiceAsync(service);
        return $"{service.Title} created";
    }
}