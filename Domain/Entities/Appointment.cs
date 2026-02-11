using Domain.Enums;

namespace Domain.Entities
{
    public class Appointment
    {
        public Guid AppointmentId { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;
        public string Title { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public AppointmentStatus Status { get; private set; }

        public ICollection<AppointmentServiceLink> AppointmentServices { get; private set; } = new List<AppointmentServiceLink>();

        private Appointment() { } // For EF

        public Appointment(Guid userId, DateTime startTime, DateTime endTime, string title)
        {
            if (startTime == default || endTime == default)
                throw new ArgumentException("StartTime and EndTime are required");
            if (endTime <= startTime)
                throw new ArgumentException("EndTime must be after StartTime");

            AppointmentId = Guid.NewGuid();
            UserId = userId;
            StartTime = startTime;
            EndTime = endTime;
            Title = title;
            Status = AppointmentStatus.Reserved;
        }

        public void AddServices(IEnumerable<Service> services)
        {
            foreach (var service in services)
            {
                AppointmentServices.Add(new AppointmentServiceLink(AppointmentId, service.Id));
            }
        }

        public void Cancel()
        {
            if (Status == AppointmentStatus.Cancelled)
                throw new InvalidOperationException("Already cancelled");

            Status = AppointmentStatus.Cancelled;
        }

        public void Edit(DateTime startTime, DateTime endTime)
        {
            if (startTime == default || endTime == default)
                throw new ArgumentException("StartTime and EndTime are required");
            if (endTime <= startTime)
                throw new ArgumentException("EndTime must be after StartTime");

            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
