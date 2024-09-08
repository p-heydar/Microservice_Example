using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public sealed record ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new IdCannotEmptyException(nameof(ProductId));
        return new ProductId(value);
    }
}