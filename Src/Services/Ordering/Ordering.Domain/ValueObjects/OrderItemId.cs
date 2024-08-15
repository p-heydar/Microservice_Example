using System.Runtime.CompilerServices;
using Ordering.Domain.Models;

namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value) => this.Value = value;

    public static OrderItemId Of(Guid orderItemId)
    {
        if (Guid.Empty == orderItemId)
            throw new DomainException("Order Item Id Cannot Be Emtpy !!");

        return new OrderItemId(orderItemId);
    }
}