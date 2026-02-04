public class Appointment
{
    public Guid AppointmentId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ServiceId { get; private set; }
    public string Title { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public AppointmentStatus Status { get; private set; }

    private Appointment()
    {
    }

    public Appointment(Guid userId, Guid serviceId, DateTime starttime, DateTime endtime,string title)
    {
        if (starttime == default || endtime == default)
            throw new ArgumentException("StartTime and EndTime are required");

        if (endtime <= starttime)
            throw new ArgumentException("EndTime must be after StartTime");

        AppointmentId = Guid.NewGuid();
        UserId = userId;
        ServiceId = serviceId;
        StartTime = starttime;
        EndTime = endtime;
        Title = title;
        Status = AppointmentStatus.Reserved;
    }

    public void Cancel()
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Already cancelled");

        Status = AppointmentStatus.Cancelled;
    }
    public void Edit(DateTime starttime, DateTime endtime)
    {
        StartTime = starttime;
        EndTime = endtime;
    }
}

public enum AppointmentStatus
{
    Reserved,
    Cancelled,
    Completed
}