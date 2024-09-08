namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId>:Entity<TId>, IAggregate<TId>
{
    public List<IDomainEvent> DomainEvents { get; private set; } = new();

    public List<IDomainEvent> AddDomainEvents(IDomainEvent domainEvent)
    {
        this.DomainEvents.Add(domainEvent);
        return this.DomainEvents;
    }


    public List<IDomainEvent> ClearDomainEvents()
    {
        var dequeueEvents = this.DomainEvents;

        this.ClearDomainEvents();

        return dequeueEvents;
    }
}