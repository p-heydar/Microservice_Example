using Discount.Grpc.Properties.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext:DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
    
    public DiscountContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, Amount = 2000, ProductName = "Iphone 13", Description = "Iphone 13 SX" },
            new Coupon { Id = 2, Amount = 4000, ProductName = "Iphone 14", Description = "Iphone 14 SX" }
        );
    }
}