using Domain.Base;

namespace Domain.Events.Service;

public record ServiceCreatedEvent(
    Guid ServiceId,
    string ServiceName,
    TimeSpan DurationTime):DomainEvent;