using Domain.Base;

namespace Domain.Events.Appointment;

public record AppointmentCompletedEvent(
    Guid AppointmentId,
    Guid ServiceId,
    Guid UserId) : DomainEvent;