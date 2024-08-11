namespace Ordering.Domain.Abstracions;

public interface IAggregate<T> : IEntity<T>, IAggregate
{
    
}

public interface IAggregate:IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] clearDomainEvents();
}