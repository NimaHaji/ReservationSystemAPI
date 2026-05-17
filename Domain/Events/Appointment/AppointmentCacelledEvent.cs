using Domain.Base;

namespace Domain.Events.Appointment;

public record AppointmentCancelledEvent(
    Guid AppointmentId,
    Guid ServiceId,
    Guid UserId) : DomainEvent;