namespace Domain;

public class Service
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public int DurationMinutes { get; private set; }

    private Service() { }

    public Service(string title, int durationMinutes)
    {
        if (durationMinutes <= 0)
            throw new ArgumentException("Invalid duration");

        Id = Guid.NewGuid();
        Title = title;
        DurationMinutes = durationMinutes;
    }

    public void Edit(int durationMinutes,string title)
    {
        DurationMinutes = durationMinutes;
        Title = title;
    }
}