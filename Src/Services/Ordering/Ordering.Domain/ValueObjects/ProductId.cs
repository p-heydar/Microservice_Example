namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }
    private ProductId(Guid value) => this.Value = value;

    public static ProductId Of(Guid productId)
    {
        if (Guid.Empty == productId)
            throw new DomainException("Product Id Cannot Be Empy Value!!");
        
        return new ProductId(productId);
    }
}