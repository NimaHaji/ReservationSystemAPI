namespace Application;

public class CreateAppointment
{
    public Guid UserID { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string AppoinmentTitle { get; set; }
}