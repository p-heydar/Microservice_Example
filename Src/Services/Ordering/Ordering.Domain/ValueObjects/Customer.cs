using Ordering.Domain.Abstractions;

namespace Ordering.Domain.ValueObjects;

public sealed class Customer:Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    private Customer(CustomerId id, string name, string email)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
    }

    public static Customer of(CustomerId id, string name, string email)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        
        return new(id, name, email);
    }
    
    
}