using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public sealed record CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new IdCannotEmptyException(nameof(CustomerId));
        return new CustomerId(value);
    }
}