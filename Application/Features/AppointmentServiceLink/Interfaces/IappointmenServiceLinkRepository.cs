namespace Application.Features.AppointmentServiceLink.Interfaces;

public interface IAppointmenServiceLinkRepository
{
    Task<List<Guid>> GetServiceIdsByAppointmentId(Guid appointmentId);
    Task AddRangeAsync(Guid appointmentId, IEnumerable<Guid> serviceIds);
    Task RemoveRangeAsync(Guid appointmentId, IEnumerable<Guid> serviceIds);
    Task SaveAsync();
}