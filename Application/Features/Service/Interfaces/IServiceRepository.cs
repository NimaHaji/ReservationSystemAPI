using Application.Features.Service.DTOs;

namespace Application.Features.Service.Interfaces;

public interface IServiceRepository
{
    Task CreatServiceAsync(Domain.Entities.Service service);
    Task SaveChangesAsync();
    Task<List<ViewServices>> ViewAllServiceAsync();
    Task<Domain.Entities.Service?> GetServiceByIdAsync(Guid serviceId);
    Task<List<Domain.Entities.Service>> GetServiceListByIdsAsync(List<Guid> serviceIds);
}   