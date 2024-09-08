namespace Ordering.Domain.Abstractions;

public interface IAggregate<TId> : IAggregate, IEntity<TId> { }

public interface IAggregate:IEntity
{
    List<IDomainEvent> DomainEvents { get; }
    List<IDomainEvent> ClearDomainEvents();
}