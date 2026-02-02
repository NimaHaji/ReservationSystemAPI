using System.Security.AccessControl;
using Domain;

namespace Application;

public class CreateAppointment
{
    public Guid UserID { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public UserRole UserRole { get; set; }
    
}