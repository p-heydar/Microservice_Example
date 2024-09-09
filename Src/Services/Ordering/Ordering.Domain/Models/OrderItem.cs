using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public sealed class OrderItem:Entity<OrderItemId>
{
    private OrderItem()
    {
        
    }
    private OrderItem(OrderItemId orderItemId, OrderId orderId, ProductId productId, int quantity, decimal price)
    {
        Id = orderItemId;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public static OrderItem Create(OrderItemId orderItemId, OrderId orderId, ProductId productId, int quantity, decimal price)
    {
        return new OrderItem(orderItemId, orderId, productId, quantity, price);
    }

    public OrderId OrderId { get; private set; }

    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
}