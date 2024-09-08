using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public sealed record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value)
    {
        this.Value = value;
    }

    public static OrderId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new IdCannotEmptyException(nameof(OrderId));
        return new OrderId(value);
    }
}