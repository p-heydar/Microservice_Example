using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public sealed class CustomerConfiguration:IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value,
            customerId => CustomerId.Of(customerId));

        builder.Property(x => x.Name).HasMaxLength(100);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).HasMaxLength(50);
        
    }
}