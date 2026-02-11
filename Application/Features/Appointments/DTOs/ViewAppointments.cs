namespace Application.Features.Appointments.DTOs;

public class ViewAppointments
{
    public string AppointmentsTitle { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; }
    public List<string> Services { get; set; }=new ();
}