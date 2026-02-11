using Application.Features.Service.DTOs;

namespace Application.Features.Service.Services;

public interface IServiceAppService
{
    Task<string> CreateServiceAsync(CreateService createService);
    Task<string> EditServiceAsync(Guid serviceId,EditService editService);
    Task<List<ViewServices>> ViewAllServicesAsync();
}