using Domain.Base;

namespace Domain.Events.Appointment;

public record AppointmentRescheduledEvent(
    Guid AppointmentId,
    Guid ServiceId,
    Guid UserId) : DomainEvent;