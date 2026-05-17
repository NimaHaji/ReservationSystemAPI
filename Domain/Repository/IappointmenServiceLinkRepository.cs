namespace Domain.Repository;

public interface IAppointmenServiceLinkRepository
{
    Task<List<Guid>> GetServiceIdsByAppointmentId(Guid appointmentId);
    Task AddRangeAsync(Guid appointmentId, IEnumerable<Guid> serviceIds);
    Task RemoveRangeAsync(Guid appointmentId, IEnumerable<Guid> serviceIds);
    Task SaveAsync();
}