using Domain.Aggregates.Service;

namespace Domain.Repository;

public interface IServiceRepository
{
    Task AddServiceAsync(Service service);
    Task DeleteServiceAsync(Guid serviceId);
    Task<List<Service>> ViewAllServiceAsync();
    Task<Service?> GetServiceByIdAsync(Guid serviceId);
    Task<List<Service>> GetServiceListByIdsAsync(List<Guid> serviceIds);
}   