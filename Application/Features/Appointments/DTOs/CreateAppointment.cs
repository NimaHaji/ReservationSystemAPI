using System.ComponentModel.DataAnnotations;

namespace Application.Features.Appointments.DTOs;

public class CreateAppointment
{
    [Required]
    public List<Guid> ServiceId { get; set; } = new ();
    [Required]    
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public string AppoinmentTitle { get; set; }
}