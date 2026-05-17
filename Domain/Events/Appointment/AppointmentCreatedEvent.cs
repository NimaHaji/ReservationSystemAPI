using Domain.Base;

namespace Domain.Events.Appointment;

public record AppointmentCreatedEvent(
    Guid AppointmentId,
    Guid ServiceId,
    Guid UserId) : DomainEvent;