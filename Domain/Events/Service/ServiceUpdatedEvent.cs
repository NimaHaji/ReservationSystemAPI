using Domain.Base;

namespace Domain.Events.Service;

public record ServiceUpdatedEvent(
    Guid ServiceId,
    string ServiceName,
    TimeSpan DurationTime):DomainEvent;