using Domain.Base;
using Domain.Enums;
using Domain.ValueObject;

namespace Domain.Aggregates.Appointment
{
    public class Appointment:AggregatedRoot
    {
        public Guid AppointmentId { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public AppointmentTimeRange TimeRange { get; private set; }
        public AppointmentStatus Status { get; private set; }

        private readonly List<Guid> _serviceIds = new();
        public IReadOnlyList<Guid> ServiceIds=>_serviceIds;

        public void AddServices(IEnumerable<Guid> serviceIds)
        {
            foreach (var id in serviceIds.Distinct())
            {
                if (!_serviceIds.Contains(id))
                    _serviceIds.Add(id);
            }
        }

        private Appointment() { } // For EF

        public Appointment(Guid userId,AppointmentTimeRange timeRange, string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be null or empty");
            AppointmentId = Guid.NewGuid();
            UserId = userId;
            TimeRange = timeRange??throw new ArgumentNullException(nameof(timeRange))   ;
            Title = title;
            Status = AppointmentStatus.Reserved;
        }

        public void Cancel()
        {
            if (Status == AppointmentStatus.Cancelled)
                throw new InvalidOperationException("Already cancelled");

            Status = AppointmentStatus.Cancelled;
        }

        public void Reschedule(AppointmentTimeRange timeRange)
        {
            if (Status == AppointmentStatus.Cancelled)
                throw new InvalidOperationException("Cannot reschedule cancelled appointment");
            TimeRange = timeRange ?? throw new ArgumentNullException(nameof(timeRange));
        }
        public void ChangeTitle(string title)
        {
            if (Status == AppointmentStatus.Cancelled)
                throw new InvalidOperationException("Cannot edit cancelled appointment");
            
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be null or empty");
            Title=title;
        }
    }
}
