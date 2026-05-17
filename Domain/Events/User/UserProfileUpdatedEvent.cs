using Domain.Base;

namespace Domain.Events.User;

public record UserProfileUpdatedEvent(
    Guid UserId,
    string NewFullName):DomainEvent;