
namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }
    private CustomerId(Guid value) => this.Value = value;

    public static CustomerId Of(Guid newValue)
    {
        ArgumentNullException.ThrowIfNull(newValue);
        if (newValue == Guid.Empty)
            throw new DomainException("CustomerId Cannot Be Empty!");
        return new CustomerId(newValue);
    }
}