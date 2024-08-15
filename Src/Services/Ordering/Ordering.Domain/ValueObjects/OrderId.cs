namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value) => this.Value = value;

    public static OrderId Of(Guid orderId)
    {
        if (Guid.Empty == orderId)
            throw new DomainException("Order Id Cannot Be Empy Value!!");
        
        return new OrderId(orderId);
    }
}