namespace Ordering.Domain.ValueObjects;

public sealed record OrderName
{
    public string Value { get; }

    private OrderName(string value)
    {
        Value = value;
    }

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value.Length, 5);
        
        return new OrderName(value);
    }
}