using Domain.Base;

namespace Domain.Events.User;

public record UserRegisteredEvent(
    Guid UserId,
    string Email):DomainEvent;