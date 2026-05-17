using Domain.Base;

namespace Domain.Aggregates.Service;

public class Service:AggregatedRoot
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public TimeSpan DurationMinutes { get; private set; }
    private Service() { }

    public Service(string title, TimeSpan durationMinutes)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("Title required");
        if (durationMinutes.TotalMinutes <= 0)
            throw new ArgumentException("Invalid duration");
        Id = Guid.NewGuid();
        Title = title;
        DurationMinutes = durationMinutes;
    }

    public void Edit(TimeSpan durationMinutes,string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("Title required");
        if (durationMinutes.TotalMinutes <= 0)
            throw new ArgumentException("Invalid duration");
        DurationMinutes = durationMinutes;
        Title = title;
    }
}