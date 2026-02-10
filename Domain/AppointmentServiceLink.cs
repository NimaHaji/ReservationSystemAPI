namespace Domain;

public class AppointmentServiceLink
{
    public Guid AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
    public Guid ServiceId { get; set; }
    public Service Service { get; set; }
    
    private AppointmentServiceLink() { } 

    public AppointmentServiceLink(Guid appointmentId, Guid serviceId)
    {
        AppointmentId = appointmentId;
        ServiceId = serviceId;
    }
}