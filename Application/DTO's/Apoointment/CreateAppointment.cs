using System.ComponentModel.DataAnnotations;
using Domain;

namespace Application;

public class CreateAppointment
{
    [Required]
    public Guid UserID { get; set; }
    [Required]
    public List<Guid> ServiceId { get; set; } = new ();
    [Required]    
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public string AppoinmentTitle { get; set; }
}