using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public sealed record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value)
    {
        Value = value;
    }

    public static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new IdCannotEmptyException(nameof(OrderItemId));
        return new(value);
    }
}