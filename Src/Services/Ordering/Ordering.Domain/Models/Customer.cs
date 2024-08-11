namespace Ordering.Domain.Models;

public class Customer:Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public static Customer Create(CustomerId newId, string newName, string newEmail)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(newName);
        ArgumentNullException.ThrowIfNullOrEmpty(newEmail);

        var customer = new Customer()
        {
            Id = newId,
            Email = newEmail,
            Name = newName
        };
        return customer;
    }
}