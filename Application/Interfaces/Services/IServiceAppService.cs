using Application.DTO_s.Service;

namespace Application.Interfaces;

public interface IServiceAppService
{
    Task<string> CreateServiceAsync(CreateService createService);
    Task<string> EditServiceAsync(Guid serviceId,EditService editService);
    Task<List<ViewServices>> ViewAllServicesAsync();
}