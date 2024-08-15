namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    public const int DefaultLength = 5;
    public string value { get; }
    public OrderName(string value) => value = value;

    public static OrderName Of(string newValue)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newValue);
        ArgumentOutOfRangeException.ThrowIfNotEqual(newValue.Length, DefaultLength);

        return new OrderName(newValue);
    }
}