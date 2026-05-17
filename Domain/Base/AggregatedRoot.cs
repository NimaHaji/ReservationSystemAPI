namespace Domain.Base;

public class AggregatedRoot:EntityBase
{
    private readonly List<DomainEvent> _domainEvents=[];
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents;
    
    protected void AddDomainEvent(DomainEvent domainEvent)=> _domainEvents.Add(domainEvent);
    
    public void ClearDomainEvents() => _domainEvents.Clear();
}