namespace Application.Features.Appointments.DTOs;

public class EditAppointment
{
    public DateTime StartTime { get;  set; }
    public DateTime EndTime { get;  set; }
    public List<Guid> ServiceIds { get; set; } = [];
}