using Domain.Base;

namespace Domain.Events.User;

public record UserRemovedEvent(
    Guid UserId):DomainEvent;