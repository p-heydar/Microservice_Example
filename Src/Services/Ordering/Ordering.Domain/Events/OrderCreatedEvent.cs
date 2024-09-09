using Ordering.Domain.Abstractions;
using Ordering.Domain.Models;

namespace Ordering.Domain.Events;

public sealed record OrderCreatedEvent(Order order):IDomainEvent;