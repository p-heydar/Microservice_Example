using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public sealed class OrderConfiguration:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value,
            orderId => OrderId.Of(orderId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId);

        builder.HasMany(x => x.OrderItems)
            .WithOne()
            .HasForeignKey(x => x.OrderId);

        builder.ComplexProperty(x => x.OrderName, config =>
        {
            config.Property(x => x.Value)
                .HasMaxLength(50)
                .HasColumnName(nameof(Order.OrderName))
                .IsRequired();
        });

        builder.ComplexProperty(x => x.ShippingAddress, builder =>
        {
            builder.Property(x => x.AddressLine)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.Country)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.State)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.ZipCode)
                .HasMaxLength(40)
                .IsRequired();
        });
        
        builder.ComplexProperty(x => x.BillingAddress, builder =>
        {
            builder.Property(x => x.AddressLine)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.Country)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.State)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(x => x.ZipCode)
                .HasMaxLength(40)
                .IsRequired();
        });

        builder.Property(x => x.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(x => x.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(x => x.TotalPrice);
    }
}