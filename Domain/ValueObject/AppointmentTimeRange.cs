namespace Domain.ValueObject;

public class AppointmentTimeRange
{
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public AppointmentTimeRange(DateTime startTime, DateTime endTime)
    {
        if (startTime == default || endTime == default)
            throw new ArgumentException("Start and End are required");
        if(startTime >= endTime)
            throw new ArgumentException("Start time must be before end time");
        StartTime = startTime;
        EndTime = endTime;
    }
}