namespace Ordering.Domain.Abstractions;

public interface IEntity<T>:IEntity
{
    public T Id { get; set; }
}

public interface IEntity
{
    // Create
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    
    // Modify
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}