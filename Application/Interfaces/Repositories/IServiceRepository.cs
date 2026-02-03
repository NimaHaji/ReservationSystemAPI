using Application.DTO_s.Service;
using Domain;

namespace Application.Interfaces.Repositories;

public interface IServiceRepository
{
    Task CreatServiceAsync(Service service);
    Task SaveChangesAsync();
}